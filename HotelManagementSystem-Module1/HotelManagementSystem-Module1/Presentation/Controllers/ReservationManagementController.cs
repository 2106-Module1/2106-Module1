using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        private readonly IGuestService _guestService;
        private readonly IAuthenticate _authenticationService;
        private readonly IRoom _roomService;


        // Implementing code together with Mod 1 Team 6 Authentication Service
        public ReservationManagementController(IReservationService reservationService, IPromoCodeService promoCodeService, 
            IGuestService guestService, IAuthenticate authenticateService, IRoom roomService)
        {
            _reservationService = reservationService;
            _promoCodeService = promoCodeService;

            // Calling Mod 1 Team 9 Service - for guest details
            _guestService = guestService;

            // Calling Mod 1 Team 6 Service - for authentication of secret pin
            _authenticationService = authenticateService;
            _roomService = roomService;
        }

        /*
         * <summary>
         * Function to retrieve a single Reservation Record to display in detail.
         * This function will link to Update Reservation if there is a need to update.
         * TODO for Deliverable 3
         * </summary>
         */
        [HttpGet]
        public IActionResult UpdateReservation()
        {
            int resId = Convert.ToInt32(Request.Query["resId"]);
            Dictionary<string, object> resRecord = _reservationService.SearchByReservationId(resId).GetReservation();

            Guest g = _guestService.SearchByGuestId((int)resRecord["guestID"]);

            ViewBag.resID = resId;
            ViewBag.ResRecord = resRecord;
            ViewBag.GuestName = g.FirstNameDetails() + " " + g.LastNameDetails();
            ViewBag.GuestEmail = g.EmailDetails();
            return View();
        }

        [HttpPost]
        public IActionResult UpdateReservation(IFormCollection resForm)
        {

            // To remove once Mod 1 Team 6 passes uh room + price details
            var roomDetailDict = new Dictionary<string, double>()
            {
                { "Twin", 100.0 },
                { "Double", 150.0 },
                { "Family", 300.0 },
                { "Suite", 600.0 }
            };

            double finalPrice;

            int resId = Convert.ToInt32(resForm["resID"]);
            int pax = Convert.ToInt32(resForm["Number of Guests"]);
            string roomType = resForm["Room Type"];
            DateTime startDate = Convert.ToDateTime(resForm["Check-In Date/Time"]);
            DateTime endDate = Convert.ToDateTime(resForm["Check-Out Date/Time"]);
            string remarks = resForm["Remarks"];
            DateTime modifiedDate = DateTime.Now;
            string promoCode = resForm["PromoCode"];
            string status = resForm["Status"];

            if (resForm["submit"].ToString() == "Delete")
            {
                string checkStatus = _reservationService.SearchByReservationId(resId).GetReservation()["status"].ToString();
                if (checkStatus == "Cancelled")
                {
                    _reservationService.DeleteReservation(resId);
                    TempData["Message"] = "Record has been deleted";
                    return RedirectToAction("ReservationView", "Reservation");
                }
                TempData["Message"] = "Invalid Access to delete Reservation Record!";
                return RedirectToAction("UpdateReservation", "ReservationManagement", new { resID = resId });
            }
            else
            {
                // Validate Num of Guest against Room Type Capacity
                if (!RoomTypeToGuestNum(roomType, pax))
                {
                    TempData["Message"] = "ERROR: " + roomType + " room is unable to hold " + pax + " guests.";
                    return RedirectToAction("UpdateReservation", "ReservationManagement", new { resID = resId });
                }

                // Validate Reservation Dates
                int dateFlag = CheckDates(startDate, endDate);

                if (dateFlag == 1)
                {
                    TempData["Message"] = "ERROR: Start Date is more than End Date";
                    return RedirectToAction("UpdateReservation", "ReservationManagement", new { resID = resId });
                }
                if (dateFlag == 2)
                {
                    TempData["Message"] = "ERROR: Current Date is more than Start Date";
                    return RedirectToAction("UpdateReservation", "ReservationManagement", new { resID = resId });
                }

                // Retrieve price by room type
                var initialPrice = roomDetailDict[roomType];

                // Check if there is a Promo Code given
                if (promoCode != "")
                {
                    // Validate if given Promo Code is valid
                    PromoCode resPromoCode = _promoCodeService.GetPromoCode(promoCode);
                    if (resPromoCode == null)
                    {
                        TempData["CreateReservationMsg"] = "Invalid Promo Code";
                        return RedirectToAction("UpdateReservation", "ReservationManagement", new { resID = resId });
                    }
                    // get the last two digit of the promo Code which will be the discount % and factor into room price
                    var discount = (int)resPromoCode.GetPromoCode()["discount"];
                    finalPrice = initialPrice - (initialPrice * (discount / 100.0));
                }
                else
                {
                    finalPrice = initialPrice;
                }

                // Update Database 
                _reservationService.UpdateReservation(resId, pax, roomType, startDate, endDate, remarks, modifiedDate, promoCode, finalPrice, status);

                // Success Message
                TempData["Message"] = "Status updated Successfully";
                return RedirectToAction("ReservationView", "Reservation");
            }
        }

        [HttpPost]
        public IActionResult UpdateReservationStatus(IFormCollection statusForm)
        {
            var resId = Convert.ToInt32(statusForm["resId"]);
            string status = Convert.ToString(statusForm["Status"]);

            // call function in service to update status and return a boolean
            bool success = _reservationService.UpdateReservationStatus(resId, status);

            if (success)
            {
                // Success Message
                TempData["Message"] = "Status updated Successfully";
                return RedirectToAction("ReservationView", "Reservation");
            }
            else
            {
                // Success Message
                TempData["Message"] = "Status updated Unsuccessfully";
                return RedirectToAction("ReservationView", "Reservation");
            }

        }

        [NonAction]
        public int CheckDates(DateTime start, DateTime end)
        {
            var now = DateTime.Now;

            // Check if current date is less then reservation date
            // Check if current year <= reservation year, (current month = reservation month, current day must be less than reservation day)
            // if not (current month must be less than reservation day)
            if (now.Year <= start.Year && ((now.Month == start.Month && now.Day < start.Day) || now.Month < start.Month))
            {
                // similarly check if start date is less than end date
                if (start.Year <= end.Year && ((start.Month == end.Month && start.Day < end.Day) || start.Month < end.Month))
                {
                    return 0;
                }
                else
                {
                    // Error: Start Date is more than End Date
                    return 1;
                }
            }
            // Error: Current Date is more than Start Date
            return 2;
        }

        [NonAction]
        public bool RoomTypeToGuestNum(string roomType, int numOfGuest)
        {
            var roomCap = new Dictionary<string, int>
            {
                {"Twin", 2},
                {"Double", 2},
                {"Family", 4},
                {"Suite", 5}
            };

            if (numOfGuest > roomCap[roomType] || numOfGuest <= 0)

            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
