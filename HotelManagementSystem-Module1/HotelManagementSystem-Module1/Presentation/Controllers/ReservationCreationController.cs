using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        /*
         * Mod 2 Team 2 Service - To include after D2
         * private readonly ShuttleScheduleGateway _ShuttleScheduleGateway;
         * private readonly ShuttleService _ShuttleService;
         *
         * public ReservationCreationController(IReservationService reservationService, IGuestService guestService, ShuttleScheduleGateway ShuttleScheduleGateway)
         */

        public ReservationCreationController(IReservationService reservationService, IGuestService guestService, IPromoCodeService promoCodeService)
        {
            _guestService = guestService;
            _reservationService = reservationService;
            _promoCodeService = promoCodeService;
            /*
             * Calling Mod 2 Team 2 Service - for checking availability of transport reservation
             * _ShuttleScheduleGateway = ShuttleScheduleGateway;
             * _ShuttleService = new ShuttleService(_ShuttleScheduleGateway
             */
        }

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
            Dictionary<string, object> resTemp = new Dictionary<string, object>();
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

            // storing view Form data type to show on View Form
            resTemp.Add("Number of Guests", default(int));
            resTemp.Add("Room Type", default(string));
            resTemp.Add("Check-In Date/Time", DateTime.Now.Date.AddDays(1).AddHours(10));
            resTemp.Add("Check-Out Date/Time", DateTime.Now.Date.AddDays(2).AddHours(14));
            resTemp.Add("Remarks", default(string));
            resTemp.Add("Promotion Code", default(string));

            // Passing data over to View Page via ViewBag "/Reservation/CreateReservation"
            ViewBag.guestid = guest.GuestIdDetails();
            ViewBag.guestName = guest.FirstNameDetails() + " " + guest.LastNameDetails();
            ViewBag.guestDetail = guestDetail;

            return View(resTemp);
        }

        [HttpPost]
        public IActionResult CreateReservation(IFormCollection resForm)
        {
            // Initializing Variables
            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            double finalPrice;

            // To remove once Mod 1 Team 6 passes uh room + price details
            var roomDetailDict = new Dictionary<string, double>()
            {
                { "Twin", 100.0 },
                { "Double", 150.0 },
                { "Family", 300.0 },
                { "Suite", 600.0 }
            };

            // retrieving data from Form collection
            int guestId = Convert.ToInt32(resForm["GuestId"]);
            int noOfGuest = Convert.ToInt32(resForm["Number of Guests"]);
            string promoCode = resForm["Promotion Code"];
            string roomType = resForm["Room Type"];
            DateTime start = Convert.ToDateTime(resForm["Check-In Date/Time"]);
            DateTime end = Convert.ToDateTime(resForm["Check-Out Date/Time"]);

            // Validate Num of Guest against Room Type Capacity
            if (!RoomTypeToGuestNum(roomType, noOfGuest))
            {
                TempData["Message"] = "ERROR: " + roomType + " room is unable to hold " + noOfGuest + " guests.";
                return RedirectToAction("CreateReservation", "ReservationCreation", new { GuestId = guestId });
            }

            // Validate Reservation Dates
            int dateFlag = CheckDates(start, end);

            if (dateFlag == 1)
            {
                TempData["Message"] = "ERROR: Start Date is more than End Date";
                return RedirectToAction("CreateReservation", "ReservationCreation", new { GuestId = guestId });
            }
            else if (dateFlag == 2)
            {
                TempData["Message"] = "ERROR: Current Date is more than Start Date";
                return RedirectToAction("CreateReservation", "ReservationCreation", new { GuestId=guestId });
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
                    return RedirectToAction("CreateReservation", "ReservationCreation");
                }
                // get the last two digit of the promo Code which will be the discount % and factor into room price
                var discount = (int)resPromoCode.GetPromoCode()["discount"];
                finalPrice = initialPrice - (initialPrice * (discount / 100.0));
            }
            else
            {
                finalPrice = initialPrice;
            }

            // Add all POST data into a dictionary
            resTemp.Add("guestID", guestId);
            resTemp.Add("numOfGuest", noOfGuest);
            resTemp.Add("roomType", roomType);
            resTemp.Add("start", Convert.ToDateTime(resForm["Check-In Date/Time"]));
            resTemp.Add("end", Convert.ToDateTime(resForm["Check-Out Date/Time"]));
            resTemp.Add("remark", resForm["Remarks"].ToString());
            resTemp.Add("modified", DateTime.Now);
            resTemp.Add("promoCode", promoCode);
            resTemp.Add("price", finalPrice);
            resTemp.Add("status", "Unfulfilled");

            // Creating Reservation object and storing it to database
            Reservation createdReservation = (Reservation)new Reservation().SetReservation(resTemp);
            _reservationService.CreateReservation(createdReservation);

            // Retrieve latest Reservation ID Created
            var reservationId = Convert.ToInt32(_reservationService.GetLatestReservation().GetReservation()["resID"]);

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
