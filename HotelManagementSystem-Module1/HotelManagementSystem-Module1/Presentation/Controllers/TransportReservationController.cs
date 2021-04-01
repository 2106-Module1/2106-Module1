using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Presentation.Controllers
{
    public class TransportReservationController : Controller
    {
        private readonly IGuestService _guestService;
        private readonly IShuttleServices _shuttleServices;

        public TransportReservationController(IGuestService guestService, IShuttleServices shuttleServices)
        {
            _guestService = guestService;

            //Calling Mod 2 Team 2 Service - for checking availability of transport reservation
            _shuttleServices = shuttleServices;
        }

        [HttpGet]
        public IActionResult TransportReservation()
        {
            int guestId = Convert.ToInt32(Request.Query["GuestId"]);
            int noOfGuest = Convert.ToInt32(Request.Query["NumOfGuest"]);

            Guest g = _guestService.SearchByGuestId(guestId);

            // Return View page with resTemp data
            ViewBag.guestid = g.GuestIdDetails();
            ViewBag.guestName = g.FirstNameDetails() + " " + g.LastNameDetails();
            ViewBag.noOfGuest = noOfGuest;
            return View();
        }

        [HttpPost]
        public IActionResult TransportReservation(IFormCollection transportResForm)
        {
            var guestId = Convert.ToInt32(transportResForm["GuestId"].ToString());
            var guestNum = Convert.ToInt32(transportResForm["NumOfGuest"].ToString());
            var guestName = transportResForm["GuestName"].ToString();
            var arrivalCheck = transportResForm["Arrival"].ToString();
            var departureCheck = transportResForm["Departure"].ToString();

            if (arrivalCheck == "true")
            {
                var arrival = Convert.ToDateTime(transportResForm["Arrival Date/Time"].ToString());
                var arrivalNumOfGuest = Convert.ToInt32(transportResForm["Arrival Number of Guests"].ToString());

                var newArrivalSchedule = new ShuttleSchedule(_shuttleServices.GenerateID(arrival, guestId), arrival,
                    "Arrival", guestId, arrivalNumOfGuest, guestName);

                if (!_shuttleServices.AddGuestShuttleBooking(newArrivalSchedule.RetrieveShuttleScheduleObject()).Result)
                {
                    TempData["Message"] = "ERROR: Unavailable Transport Timing for Airport to Hotel!";
                    return RedirectToAction("TransportReservation", "TransportReservation",new { GuestId = guestId, NumOfGuest = guestNum });
                }
            }

            if (departureCheck == "true")
            {
                var departure = Convert.ToDateTime(transportResForm["Departure Date/Time"].ToString());
                var departureNumOfGuest = Convert.ToInt32(transportResForm["Departure Number of Guests"].ToString());

                var newDepartureSchedule = new ShuttleSchedule(_shuttleServices.GenerateID(departure, guestId), departure,
                    "Departure", guestId, departureNumOfGuest, guestName);

                if (!_shuttleServices.AddGuestShuttleBooking(newDepartureSchedule.RetrieveShuttleScheduleObject()).Result)
                {
                    TempData["Message"] = "ERROR: Unavailable Transport Timing for Hotel to Airport!";
                    return RedirectToAction("TransportReservation", "TransportReservation", new { GuestId = guestId, NumOfGuest = guestNum });
                }
            }

            // After completion of creation to redirect user to "/Reservation/ReservationView"
            TempData["Message"] = "Transportation Booked Successfully!";
            return Redirect("/Reservation/ReservationView");
        }
    }
}
