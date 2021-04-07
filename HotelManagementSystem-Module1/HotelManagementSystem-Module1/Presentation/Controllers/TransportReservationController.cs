using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HotelManagementSystem.Models.ConControls;

/*
 * Owner of TransportReservationController: Mod 1 Team 4
 * This Controller is used for Creating new Transport Reservations via 
 * Mod 2 Team 2 domain and data source layer only.
 */
namespace HotelManagementSystem.Presentation.Controllers
{
    public class TransportReservationController : Controller
    {
        private readonly IGuestService _guestService;
        private readonly IShuttleServices _shuttleService;
        private readonly IShuttleBusPassengerServices _shuttleBusPassengerService;

        public TransportReservationController(IGuestService guestService, IShuttleServices shuttleService,
            IShuttleBusPassengerServices shuttleBusPassengerService)
        {
            //Calling Mod 1 Team 9 Service - for getting guest 
            _guestService = guestService;

            //Calling Mod 2 Team 2 Service - for checking availability of transport reservation
            _shuttleService = shuttleService;
            _shuttleBusPassengerService = shuttleBusPassengerService;
        }

        /// <summary>
        /// (Completed)
        /// Function to retrieve query in URL and display transport reservation form
        /// </summary>
        [HttpGet]
        public IActionResult TransportReservation()
        {
            int guestId = Convert.ToInt32(Request.Query["GuestId"]);
            int noOfGuest = Convert.ToInt32(Request.Query["NumOfGuest"]);
            int resId = Convert.ToInt32(Request.Query["ResId"]);

            Guest g = _guestService.SearchByGuestId(guestId);

            // Return View page with resTemp data
            ViewBag.guestid = g.GuestIdDetails();
            ViewBag.guestName = g.FirstNameDetails() + " " + g.LastNameDetails();
            ViewBag.noOfGuest = noOfGuest;
            ViewBag.resid = resId;
            return View();
        }

        /// <summary>
        /// (Completed)
        /// Function to create Transport Reservation Record via
        /// Mod 2 Team 2 domain and data source layer only.
        /// </summary>
        /// <param name="transportResForm">Form data parse from client side via POST request</param>
        [HttpPost]
        public IActionResult TransportReservation(IFormCollection transportResForm)
        {
            var resId = Convert.ToInt32(transportResForm["ResId"].ToString());
            var guestId = Convert.ToInt32(transportResForm["GuestId"].ToString());
            var guestNum = Convert.ToInt32(transportResForm["NumOfGuest"].ToString());
            var guestName = transportResForm["GuestName"].ToString();
            var arrivalCheck = transportResForm["Arrival"].ToString();
            var departureCheck = transportResForm["Departure"].ToString();

            // check if user wants arrival transport reservation
            if (arrivalCheck == "true")
            {
                var arrival = Convert.ToDateTime(transportResForm["Arrival Date/Time"].ToString());
                var arrivalNumOfGuest = Convert.ToInt32(transportResForm["Arrival Number of Guests"].ToString());

                string arrivalSchId = _shuttleService.GenerateID(guestId, "Arrival");

                // create dictionary object for arrival 
                Dictionary<string, object> arrivalDict = new Dictionary<string, object>
                {
                    { "id", arrivalSchId },
                    { "ScheduleDateTime", arrival },
                    { "TravelDirection", "Arrival" },
                    { "GuestId", guestId },
                    { "NumberOfPassengers", arrivalNumOfGuest },
                    { "GuestName", guestName }
                };

                // convert dictionary into a shuttle schedule object using Mod 2 Team 2 Adapter
                IShuttleAdapter arrivalShuttleAdapter = new ShuttleScheduleAdapter(arrivalDict);
                ShuttleSchedule.ReadOnly arrivalSchedule = arrivalShuttleAdapter.GetShuttleSchedule().RetrieveShuttleScheduleObject();

                // call upon mod 2 team 2 function to check for availability
                if (_shuttleService.CheckAvailabilityForDateAndTime(arrivalSchedule.ScheduleDateTime,
                    arrivalSchedule.TravelDirection, arrivalSchedule.NumberOfPassengers))
                {
                    // create an shuttle schedule object based on the to input into database
                    ShuttleSchedule arrivalShuttleSchedule = new ShuttleSchedule(arrivalSchId, arrival, "Arrival",
                        guestId, arrivalNumOfGuest, guestName, "CREATED");

                    // add shuttle object into Mod 2 Team 2 shuttle schedule table
                    _shuttleService.AddGuestShuttleBooking(arrivalShuttleSchedule);
                }
                else
                {
                    TempData["Message"] = "ERROR: Unavailable Transport Timing for Airport to Hotel!";
                    return RedirectToAction("TransportReservation", "TransportReservation", new { GuestId = guestId, NumOfGuest = guestNum });
                }
            }

            // check if user wants departure transport reservation
            if (departureCheck == "true")
            {
                var departure = Convert.ToDateTime(transportResForm["Departure Date/Time"].ToString());
                var departureNumOfGuest = Convert.ToInt32(transportResForm["Departure Number of Guests"].ToString());

                string depSchId = _shuttleService.GenerateID(guestId, "Departure");

                Dictionary<string, object> departureDict = new Dictionary<string, object>
                {
                    { "id", depSchId },
                    { "ScheduleDateTime", departure },
                    { "TravelDirection", "Departure" },
                    { "GuestId", guestId },
                    { "NumberOfPassengers", departureNumOfGuest },
                    { "GuestName", guestName }
                };

                // convert dictionary into a shuttle schedule object using Mod 2 Team 2 Adapter
                IShuttleAdapter departureShuttleAdapter = new ShuttleScheduleAdapter(departureDict);
                ShuttleSchedule.ReadOnly departureSchedule = departureShuttleAdapter.GetShuttleSchedule().RetrieveShuttleScheduleObject();

                // call upon mod 2 team 2 function to check for availability
                if (_shuttleService.CheckAvailabilityForDateAndTime(departureSchedule.ScheduleDateTime,
                    departureSchedule.TravelDirection, departureSchedule.NumberOfPassengers))
                {
                    // create an shuttle schedule object based on the to input into database
                    ShuttleSchedule departureShuttleSchedule = new ShuttleSchedule(depSchId, departureSchedule.ScheduleDateTime, departureSchedule.TravelDirection,
                        departureSchedule.GuestId, departureSchedule.NumberOfPassengers, departureSchedule.GuestName, "CREATED");

                    // add shuttle object into Mod 2 Team 2 shuttle schedule table
                    _shuttleService.AddGuestShuttleBooking(departureShuttleSchedule);
                }
                else
                {
                    TempData["Message"] = "ERROR: Unavailable Transport Timing for Hotel to Airport!";
                    return RedirectToAction("TransportReservation", "TransportReservation", new { GuestId = guestId, NumOfGuest = guestNum });
                }
            }

            // After completion of creation to redirect user to "/Reservation/ReservationView"
            TempData["Message"] = "Transportation Booked Successfully!";
            return Redirect("/ReservationManagement/UpdateReservation?resid=" + resId);
        }
    }
}
