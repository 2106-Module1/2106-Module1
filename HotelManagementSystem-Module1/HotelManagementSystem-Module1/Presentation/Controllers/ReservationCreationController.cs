using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using HotelManagementSystem_Module1.Domain.Models;
using Microsoft.AspNetCore.Mvc;

/*
 * Owner of ReservationCreationController: Mod 1 Team 4
 * This Controller is used for Creating Reservations only.
 */
namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class ReservationCreationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IGuestService _guestService;
        /*
         * Mod 2 Team 2 Service - To include after D2
         * private readonly ShuttleScheduleGateway _ShuttleScheduleGateway;
         * private readonly ShuttleService _ShuttleService;
         *
         * public ReservationCreationController(IReservationService reservationService, IGuestService guestService, ShuttleScheduleGateway ShuttleScheduleGateway)
         */

        public ReservationCreationController(IReservationService reservationService, IGuestService guestService)
        {
            _guestService = guestService;
            _reservationService = reservationService;
            /*
             * Calling Mod 2 Team 2 Service - for checking availability of transport reservation
             * _ShuttleScheduleGateway = ShuttleScheduleGateway;
             * _ShuttleService = new ShuttleService(_ShuttleScheduleGateway
             */
        }

        public IActionResult guestSelection()
        {
            TempData["CreateGuestMessage"] = "Success";
            return RedirectToAction("Index", "Guest");
        }

        [HttpGet]
        public IActionResult CreateReservation()
        {
            // Initialising Variables 
            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            Dictionary<string, int> guestDetail = new Dictionary<string, int>();
            List<string> guestName = new List<string>();
            string[] resFields = { "Number of Guests", "Room Type", "Check-In Date/Time", "Check-Out Date/Time", "Remarks", "Promotion Code", "Price" };

            // Retrieve all existing guests based on Mod 1 Team 9 function
            IEnumerable<Guest> guestList = _guestService.RetrieveGuests();

            // For loop to store existing guest to populate View Form DropDownList
            foreach (var guest in guestList)
            {
                guestName.Add(guest.FirstNameDetails() + " " + guest.LastNameDetails());
                guestDetail.Add((guest.FirstNameDetails() + " " + guest.LastNameDetails()), guest.GuestIdDetails());
            }

            // storing view Form data type to show on View Form
            resTemp.Add("Guest Name", default(string));
            resTemp.Add("Number of Guests", default(int));
            resTemp.Add("Room Type", default(string));
            resTemp.Add("Check-In Date/Time", DateTime.Now.Date.AddHours(10));
            resTemp.Add("Check-Out Date/Time", DateTime.Now.Date.AddHours(14));
            resTemp.Add("Remarks", default(string));
            resTemp.Add("Promotion Code", default(string));
            resTemp.Add("Price", default(double));

            // Passing data over to View Page via ViewBag "/Reservation/CreateReservation"
            ViewBag.guestName = guestName;
            ViewBag.reservationTemp = resTemp;
            ViewBag.guestDetail = guestDetail;

            return View(resTemp);
        }

        [HttpPost]
        public IActionResult CreateReservation(Dictionary<string, object> newReservation)
        {
            // Initialising Variables
            IEnumerable<Guest> guestList = _guestService.RetrieveGuests();
            Dictionary<string, int> guestDetail = new Dictionary<string, int>();

            // To remove for loop once Mod 1 Team 9 pass us the Guest ID
            foreach (var guest in guestList)
            {
                guestDetail.Add((guest.FirstNameDetails() + " " + guest.LastNameDetails()), guest.GuestIdDetails());
            }

            Dictionary<string, object> resTemp = new Dictionary<string, object>();

            // Add all POST data into a dictionary
            int GuestId = guestDetail[Request.Form["Guest Name"]]; // to be changed once Mod 1 Team 9 pass us the Guest ID
            int NoOfGuest = Convert.ToInt32(Request.Form["Number of Guests"].ToString());
            resTemp.Add("guestID", GuestId);
            resTemp.Add("numOfGuest", NoOfGuest);
            resTemp.Add("roomType", Request.Form["Room Type"].ToString());
            resTemp.Add("start", Convert.ToDateTime(Request.Form["Check-In Date/Time"].ToString()));
            resTemp.Add("end", Convert.ToDateTime(Request.Form["Check-Out Date/Time"].ToString()));
            resTemp.Add("remark", Request.Form["Remarks"].ToString());
            resTemp.Add("modified", DateTime.Now);
            resTemp.Add("promoCode", Request.Form["Promotion Code"].ToString());
            resTemp.Add("price", Convert.ToDouble(Request.Form["Price"].ToString()));
            resTemp.Add("status", "Not Fulfilled");

            // Creating Reservation object and storing it to database
            Reservation createdReservation = (Reservation)new Reservation().SetReservation(resTemp);
            _reservationService.CreateReservation(createdReservation);

            int ReservationId = Convert.ToInt32(_reservationService.GetLatestReservation().GetReservation()["resID"]);

            // After completion of creation to redirect user to "/Reservation/ReservationView"
            return RedirectToAction("TransportReservation", new {
                ReservationId = ReservationId,
                GuestId = GuestId,
                NoOfGuest = NoOfGuest
            });
        }

        [HttpGet]
        public IActionResult TransportReservation()
        {
            // Initialising Variables
            Dictionary<string, object> resTemp = new Dictionary<string, object>();

            int ReservationId = Convert.ToInt32(Request.Query["ReservationId"]);
            int GuestId = Convert.ToInt32(Request.Query["GuestId"]);
            int NoOfGuest = Convert.ToInt32(Request.Query["NoOfGuest"]);

            Guest g = _guestService.SearchByGuestId(GuestId);

            // storing view Form data type to show on View Form
            resTemp.Add("Reservation ID", ReservationId);
            resTemp.Add("Guest ID", g.GuestIdDetails());
            resTemp.Add("Guest Name", g.FirstNameDetails() + " " + g.LastNameDetails());
            resTemp.Add("Number of Guests", NoOfGuest);
            resTemp.Add("Transport to Hotel - Date/Time", DateTime.Now.Date);
            resTemp.Add("Transport to Airport - Date/Time", DateTime.Now.Date);

            // Return View page with resTemp data
            ViewBag.TransportTemp = resTemp;
            return View();
        }

        [HttpPost]
        public IActionResult ShuttleReservation(Dictionary<string, object> newReservation)
        {
            // Initialising Variables
            Dictionary<string, object> resTemp = new Dictionary<string, object>();

            // Add all POST data into a dictionary
            /*resTemp.Add("guestID", guestID);
            resTemp.Add("NoOfGuest", Convert.ToInt32(Request.Form["Number of Guests"].ToString()););*/
            resTemp.Add("start", Convert.ToDateTime(Request.Form["Check-In Date/Time"].ToString()));
            resTemp.Add("end", Convert.ToDateTime(Request.Form["Check-Out Date/Time"].ToString()));

            /**
             * if (MOD2TEAM2FUNC(Airport to Hotel) == true) {
             *      if (MOD2TEAM2 FUNC(Hotel to Airport) == true) {
             *          insertIntoMod2Team2 DB;
             *          return Redirect("/Reservation/ReservationView");
             *      } else {
             *          String ErrorMsg = "Unavailable Transport Timing for Hotel to Airport";
             *          return Redirect(String.Format("/Reservation/TransportReservation.aspx?Error={0}", ErrorMsg));
             *      }
             * } else {
             *      String ErrorMsg = "Unavailable Transport Timing for Airport to Hotel";
             *      return Redirect(String.Format("/Reservation/TransportReservation.aspx?Error={0}", ErrorMsg));
             * }
             */
            // After completion of creation to redirect user to "/Reservation/ReservationView"
            return Redirect("/Reservation/ReservationView");
        }
    }
}
