using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentInterfaces;

/*
 * Owner of ReservationManagementController: Mod 1 Team 4
 * This Controller is used for Updating Reservations only.
 */
namespace HotelManagementSystem.Presentation.Controllers
{
    /*
     * <summary>
     * Controller to Route Update function to update reservation details.
     * </summary>
     */
    public class ReservationManagementController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IPromoCodeService _promoCodeService;
        private readonly IReservationValidator _reservationValidator;

        // Mod 1 Team 9 Guest Service - for retrieving guest details
        private readonly IGuestService _guestService;

        // Mod 1 Team 6 Room and Authentication Service - for retrieving room and authenticate secret pin
        private readonly IRoomGateway _roomGateway;
        private readonly IAuthenticate _authenticate;

        // Mod 2 Team 7 ReservationInvoice Service - for notifying cancellation fee
        private readonly iReservationInvoice _iReservationInvoice;

        public ReservationManagementController(IReservationService reservationService,
            IPromoCodeService promoCodeService,
            IGuestService guestService, IRoomGateway roomGateway, iReservationInvoice iReservationInvoice,
            IAuthenticate authenticate)
        {
            _reservationService = reservationService;
            _promoCodeService = promoCodeService;
            _reservationValidator = new ReservationValidator(promoCodeService, guestService, roomGateway);

            // Calling Mod 1 Team 9 Service - for guest details
            _guestService = guestService;

            // Call Mod 1 Team 6 Room Service - for room instance
            _roomGateway = roomGateway;

            // Calling Mod 1 Team 6 Authentication Service - for authentication of secret pin
            _authenticate = authenticate;

            // Call Mod 2 Team 7 ReservationInvoice Service - for payment of cancellation
            _iReservationInvoice = iReservationInvoice;
        }

        /*
         * <summary>
         * Function to retrieve a single Reservation Record to display in detail.
         * This function will link to Update Reservation if there is a need to update.
         * </summary>
         */
        [HttpGet]
        public IActionResult UpdateReservation()
        {
            int resId = Convert.ToInt32(Request.Query["resId"]);
            Dictionary<string, object> resRecord = _reservationService.SearchByReservationId(resId).GetReservation();

            Guest g = _guestService.SearchByGuestId((int) resRecord["guestID"]);

            ViewBag.resID = resId;
            ViewBag.ResRecord = resRecord;
            ViewBag.GuestName = g.FirstNameDetails() + " " + g.LastNameDetails();
            ViewBag.GuestEmail = g.EmailDetails();
            return View();
        }

        [HttpPost]
        public IActionResult UpdateReservation(IFormCollection resForm)
        {
            // Retrieving POST Data and initialise variables
            double reservationPrice;
            int resId = Convert.ToInt32(resForm["resID"]);
            int pax = Convert.ToInt32(resForm["Number of Guests"]);
            string roomType = resForm["Room Type"];
            DateTime startDate = Convert.ToDateTime(resForm["Check-In Date/Time"]);
            DateTime endDate = Convert.ToDateTime(resForm["Check-Out Date/Time"]);
            string remarks = resForm["Remarks"];
            DateTime modifiedDate = DateTime.Now;
            string promoCode = resForm["PromoCode"];
            string status = resForm["Status"];
            string secretPin = resForm["PIN"];
            int numOfDays = _reservationValidator.NumOfDays(startDate, endDate);

            // Check if Manager Secret Pin is correct
            if (_authenticate.AuthenticatePin(secretPin))
            {
                // Check if cancellation fee is required
                if (status == "Cancelled")
                {
                    if (_reservationValidator.CheckCancellationFee(DateTime.Now, startDate))
                    {
                        // Cancellation fee is 90% of reserved price
                        double price =
                            Convert.ToDouble(
                                _reservationService.SearchByReservationId(resId).GetReservation()["InitialResPrice"]) *
                            0.9;

                        // Calling Mod 2 Team 7 Service to notify of cancellation fee
                        _iReservationInvoice.notifyCancellation(resId, Convert.ToDecimal(price));
                    }
                }

                // Check if reservation is able to be deleted
                if (resForm["submit"].ToString() == "Delete")
                {
                    // retrieve current status of the reservation
                    string checkStatus = _reservationService.SearchByReservationId(resId).GetReservation()["status"]
                        .ToString();

                    // check if reservation status is cancelled if user is trying to delete
                    if (checkStatus == "Cancelled")
                    {
                        _reservationService.DeleteReservation(resId);
                        TempData["Message"] = "Record has been deleted";
                        return RedirectToAction("ReservationView", "Reservation");
                    }

                    // Error Msg
                    TempData["Message"] = "Invalid Access to delete Reservation Record!";
                    return RedirectToAction("UpdateReservation", "ReservationManagement", new {resID = resId});
                }

                // Validate Num of Guest against Room Type Capacity
                if (!_reservationValidator.RoomTypeToGuestNum(roomType, pax))
                {
                    TempData["Message"] = "ERROR: " + roomType + " room is unable to hold " + pax + " guests!";
                    return RedirectToAction("UpdateReservation", "ReservationManagement", new {resID = resId});
                }

                // Validate Reservation Dates
                int dateFlag = _reservationValidator.CheckDates(startDate, endDate);

                if (dateFlag == 1)
                {
                    TempData["Message"] = "ERROR: Start Date is more than End Date";
                    return RedirectToAction("UpdateReservation", "ReservationManagement", new {resID = resId});
                }

                if (dateFlag == 2)
                {
                    TempData["Message"] = "ERROR: Current Date is more than Start Date";
                    return RedirectToAction("UpdateReservation", "ReservationManagement", new {resID = resId});
                }

                // Check if there is a Promo Code given
                if (promoCode != "")
                {
                    // Check if the Promo Code given is valid
                    if (_reservationValidator.ValidatePromo(promoCode))
                    {
                        // set discounted price
                        reservationPrice = _reservationValidator.GetDiscountPrice(roomType, numOfDays, promoCode);
                    }
                    else
                    {
                        // Promo code given in Invalid
                        TempData["CreateReservationMsg"] = "Invalid Promo Code";
                        return RedirectToAction("UpdateReservation", "ReservationManagement", new {resID = resId});
                    }
                }
                else
                {
                    // set original price
                    reservationPrice = _reservationValidator.GetRoomPrice(roomType, numOfDays);
                }

                // Update Database
                _reservationService.UpdateReservation(resId, pax, roomType, startDate, endDate, remarks, modifiedDate,
                    promoCode, reservationPrice, status);

                // Success Message
                TempData["Message"] = "Status updated Successfully";
                return RedirectToAction("ReservationView", "Reservation");
            }
            // Error - invalid duty manager pin
            TempData["Message"] = "ERROR: Invalid Duty Manager pin!";
            return RedirectToAction("UpdateReservation", "ReservationManagement", new {resID = resId});
        }

        [HttpPost]
        public IActionResult UpdateReservationStatus(IFormCollection statusForm)
        {
            var resId = Convert.ToInt32(statusForm["resId"]);
            var status = Convert.ToString(statusForm["Status"]);
            DateTime startDate = Convert.ToDateTime(statusForm["startDate"]);
            var secretPin = Convert.ToString(statusForm["PIN"]);
            var guestid = Convert.ToInt32(statusForm["guestId"]);

            // Check if 
            if (status == "Cancelled")
            {
                if (_reservationValidator.CheckCancellationFee(DateTime.Now, startDate))
                {
                    // Cancellation fee is 90% of reserved price
                    double price =
                        Convert.ToDouble(
                            _reservationService.SearchByReservationId(resId).GetReservation()["InitialResPrice"]) * 0.9;

                    // Calling Mod 2 Team 7 Service to notify of cancellation fee
                    _iReservationInvoice.notifyCancellation(resId, Convert.ToDecimal(price));
                }
            }

            // Update Database 
            if (_authenticate.AuthenticatePin(secretPin))
            {
                // call function in service to update status and return a boolean
                bool success = _reservationService.UpdateReservationStatus(resId, status);

                if (success)
                {
                    // Success Message
                    TempData["Message"] = "Status updated Successfully";
                    return RedirectToAction("ReservationView", "Reservation");
                }
                // Error Message
                TempData["Message"] = "ERROR: Status updated Unsuccessfully";
                return RedirectToAction("ReservationView", "Reservation");
            }

            // Error - Invalid duty manager pin
            TempData["Message"] = "ERROR: Invalid Duty Manager pin!";
            return RedirectToAction("ReservationView", "Reservation", new {GuestId = guestid});
        }
    }
}
