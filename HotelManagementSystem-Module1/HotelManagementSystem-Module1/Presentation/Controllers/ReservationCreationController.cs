using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using HotelManagementSystem.DataSource;
using Microsoft.AspNetCore.Http;

/*
 * Owner of ReservationCreationController: Mod 1 Team 4
 * This Controller is used for Creating Reservations only.
 */
namespace HotelManagementSystem.Presentation.Controllers
{
    public class ReservationCreationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IGuestService _guestService;
        private readonly IPromoCodeService _promoCodeService;

        private readonly IRoom _roomService;

        private readonly IRoomGateway _roomGateway;
        /*
         * Mod 2 Team 2 Service - To include after D2
         * private readonly ShuttleScheduleGateway _ShuttleScheduleGateway;
         * private readonly ShuttleService _ShuttleService;
         *
         * public ReservationCreationController(IReservationService reservationService, IGuestService guestService, ShuttleScheduleGateway ShuttleScheduleGateway)
         */

        public ReservationCreationController(IReservationService reservationService, IGuestService guestService, IPromoCodeService promoCodeService, 
            IRoom roomService, IRoomGateway roomGateway)
        {
            _guestService = guestService;
            _reservationService = reservationService;
            _promoCodeService = promoCodeService;
            _roomService = roomService;
            _roomGateway = roomGateway;
            /*
             * Calling Mod 2 Team 2 Service - for checking availability of transport reservation
             * _ShuttleScheduleGateway = ShuttleScheduleGateway;
             * _ShuttleService = new ShuttleService(_ShuttleScheduleGateway
             */
        }

        /*
         * <summary>
         * (Completed)
         * Function to create a create reservations view.
         * </summary>
         */
        [HttpGet]
        public IActionResult CreateReservation()
        {
            // Validate if GuestId is Retrieve from Reservation Records or Guest page
            int guestId;
            if (Request.Query.Count == 0 || Request.Query.ContainsKey("GuestId"))
            {
                guestId = Convert.ToInt32(Request.Query["GuestId"]);
            }
            else
            {
                // Error: User tries to access page through url
                TempData["Message"] = "ERROR: Inappropriate access to Reservation Creation Page";
                return RedirectToAction("ReservationView", "Reservation");
            }

            // Initializing Variables 
            Dictionary<string, int> guestDetail = new Dictionary<string, int>();
            
            // Retrieve guest like this only
            Guest guest = _guestService.SearchByGuestId(guestId);

            // Validate if guest Object is null - means user typed in an invalid guestId via the url
            if (guest == null)
            {
                // Error: User tries to access page through url
                TempData["Message"] = "ERROR: Guest ID " + guestId + " does not Exist!";
                return RedirectToAction("ReservationView", "Reservation");
            }

            // Passing data over to View Page via ViewBag "/Reservation/CreateReservation"
            ViewBag.guestid = guest.GuestIdDetails();
            ViewBag.guestName = guest.FirstNameDetails() + " " + guest.LastNameDetails();
            ViewBag.guestDetail = guestDetail;

            return View();
        }

        /*
         * <summary>
         * (Completed)
         * Function to retrieve POST data from form to create new reservations
         * Objects with the use of Builder Design pattern and insert into database
         * </summary>
         */
        [HttpPost]
        public IActionResult CreateReservation(IFormCollection resForm)
        {
            // Initializing Variables
            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            
            int guestId = Convert.ToInt32(resForm["GuestId"]);

            // Add all POST data into a dictionary
            resTemp.Add("guestID", guestId);
            resTemp.Add("numOfGuest", Convert.ToInt32(resForm["Number of Guests"]));
            resTemp.Add("roomType", resForm["Room Type"]);
            resTemp.Add("start", Convert.ToDateTime(resForm["Check-In Date/Time"]));
            resTemp.Add("end", Convert.ToDateTime(resForm["Check-Out Date/Time"]));
            resTemp.Add("remark", resForm["Remarks"].ToString());
            resTemp.Add("promoCode", resForm["Promotion Code"]);

            // Create Reservation Object using Builder Pattern
            IReservationBuilder builder = new NewRoomReservationBuilder(_promoCodeService, _guestService, _roomGateway);
            ReservationDirector buildDirector = new ReservationDirector();
            
            var reservation = buildDirector.BuildNewReservation(builder, resTemp);

            if (reservation == null)
            {
                TempData["Message"] = "ERROR: Invalid inputs provided, please check your input fields!";
                return RedirectToAction("CreateReservation", "ReservationCreation", new { GuestId = guestId });
            }

            // Creating Reservation object and storing it to database
            _reservationService.CreateReservation(reservation);

            // Retrieve latest Reservation ID Created
            // var reservationId = Convert.ToInt32(_reservationService.GetLatestReservation().GetReservation()["resID"]);

            // After completion of creation to redirect user to "/Reservation/ReservationView"
            TempData["Message"] = "Reservation Successfully Created";

            return RedirectToAction("ReservationView", "Reservation");
            /*return RedirectToAction("TransportReservation", new
            {
                ReservationId = reservationId,
                GuestId = guestId,
                NoOfGuest = noOfGuest
            });*/
        }

        [HttpGet]
        public IActionResult TransportReservation()
        {
            // Initializing Variables
            Dictionary<string, object> resTemp = new Dictionary<string, object>();

            int reservationId = Convert.ToInt32(Request.Query["ReservationId"]);
            int guestId = Convert.ToInt32(Request.Query["editguestid"]);
            int noOfGuest = Convert.ToInt32(Request.Query["NoOfGuest"]);

            Guest g = _guestService.SearchByGuestId(guestId);

            // storing view Form data type to show on View Form
            resTemp.Add("Reservation ID", reservationId);
            resTemp.Add("Guest ID", guestId);
            resTemp.Add("Guest Name", g.FirstNameDetails() + " " + g.LastNameDetails());
            resTemp.Add("Number of Guests", noOfGuest);
            resTemp.Add("Transport to Hotel - Date/Time", DateTime.Now.Date);
            resTemp.Add("Transport to Airport - Date/Time", DateTime.Now.Date);

            // Return View page with resTemp data
            ViewBag.TransportTemp = resTemp;
            return View();
        }

        [HttpPost]
        public IActionResult TransportReservation(IFormCollection transportResForm)
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
