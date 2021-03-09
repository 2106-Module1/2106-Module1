 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IGuestService _guestService;
        /*
         * To include after D2
         * private readonly IAuthenticate _authenticationService
         */

        /*
         * Implementing code together with Mod 1 Team 6 Authentication Service
         * public ReservationManagementController(IReservationService reservationService, IGuestService guestService, IAuthenticate authenticateService
         */
        public ReservationManagementController(IReservationService reservationService, IGuestService guestService)
        {
            _guestService = guestService;
            _reservationService = reservationService;
            /*
             * Calling Mod 1 Team 6 Service - for authentication of secret pin
             * _authenticationService = authenticateService;
             */
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
           
            int resId = Convert.ToInt32(resForm["resID"]);
            int pax = Convert.ToInt32(resForm["Number of Guests"]);
            string roomType = resForm["Room Type"];
            DateTime startDate = Convert.ToDateTime(resForm["Check-In Date/Time"]);
            DateTime endDate = Convert.ToDateTime(resForm["Check-Out Date/Time"]);
            string remarks = resForm["Remarks"];
            DateTime modifiedDate = DateTime.Now;
            string promoCode = resForm["PromoCode"];
            double price = Convert.ToDouble(resForm["Price"]);
            string status = resForm["Status"];

            if (resForm["submit"].ToString() == "Delete")
            {
                _reservationService.DeleteReservation(resId);
                TempData["Message"] = "Record has been deleted";
                return RedirectToAction("ReservationView", "Reservation");
            }
            else
            {
                // Retrieve Reservation Record and update 
                Reservation resRecord = _reservationService.SearchByReservationId(resId);
                resRecord.UpdateReservation(pax, roomType, startDate, endDate, remarks, modifiedDate, promoCode, price, status);

                // update Database 
                _reservationService.UpdateReservation(resRecord);

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

            // Retrieve Reservation Record and update 
            Reservation resRecord = _reservationService.SearchByReservationId(resId);
            resRecord.UpdateReservation(newStatus: status);

            // update Database 
            _reservationService.UpdateReservation(resRecord);

            // Success Message
            TempData["Message"] = "Status updated Successfully";
            return RedirectToAction("ReservationView", "Reservation");
        }
    }
}
