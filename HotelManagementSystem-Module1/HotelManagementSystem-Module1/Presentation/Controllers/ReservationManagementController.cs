 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using HotelManagementSystem_Module1.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/*
 * Owner of ReservationManagementController: Mod 1 Team 4
 * This Controller is used for Updating Reservations only.
 */
namespace HotelManagementSystem_Module1.Presentation.Controllers
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

            ViewBag.ResRecord = resRecord;
            ViewBag.GuestName = g.FirstNameDetails() + " " + g.LastNameDetails();
            ViewBag.GuestEmail = g.EmailDetails();
            return View();
        }

        [HttpPost]
        public IActionResult UpdateReservation(Dictionary<string, object> updateReservation, IFormCollection resForm)
        {
            int resID = Convert.ToInt32(resForm["resID"]);
            int pax = Convert.ToInt32(resForm["Number of Guest"]);
            String roomType = resForm["Room Type"];
            DateTime startDate = Convert.ToDateTime(resForm["Check-In Date/Time"]);
            DateTime endDate = Convert.ToDateTime(resForm["Check-Out Date/Time"]);
            String remarks = resForm["Remarks"];
            DateTime modifiedDate = DateTime.Now;
            String promoCode = resForm["PromoCode"];
            Double price = Convert.ToDouble(resForm["Price"]);

            Dictionary<string, object> resRecord = _reservationService.SearchByReservationId(resID).GetReservation();

            Reservation updateRes = (Reservation)new Reservation().SetReservation(resRecord);

            updateRes.UpdateReservation();



            return View();
        }
    }
}
