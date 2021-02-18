using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.DataSource;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.Domain.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections;

/*
 * Owner of ReservationController: Mod 1 Team 4
 */
namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IGuestService _guestService;

        public ReservationController(IReservationService reservationService, IGuestService guestService)
        {
            _guestService = guestService;
            _reservationService = reservationService;

        }
        public IActionResult ReservationView()
        {
            // Initialising Variables
            IEnumerable<Guest> guestList = new List<Guest>();
            ArrayList mainList = new ArrayList();

            IEnumerable<Reservation> reservationList = _reservationService.GetAllReservations();

            // For Loop to store all existing reservation with guest name and email into a ArrayList to pass to View page 
            foreach (var res in reservationList)
            {
                // Retrieving guest by id based on Mod 1 Team 9 function
                Guest g = _guestService.SearchByGuestId((int)res.GetReservation()["guestID"]);
                if (g != null)
                {
                    String[] subList =
                    {
                        res.GetReservation()["resID"].ToString(),
                        res.GetReservation()["guestID"].ToString(),
                        g.FirstNameDetails() + " " + g.LastNameDetails(),
                        g.EmailDetails(),
                        res.GetReservation()["numOfGuest"].ToString(),
                        res.GetReservation()["roomType"].ToString(),
                        res.GetReservation()["start"].ToString(),
                        res.GetReservation()["end"].ToString(),
                        res.GetReservation()["remark"].ToString(),
                        res.GetReservation()["modified"].ToString(),
                        res.GetReservation()["promoCode"].ToString(),
                        res.GetReservation()["price"].ToString(),
                        res.GetReservation()["status"].ToString()
                    };
                    mainList.Add(subList);
                }
            }

            // Passing data over to View Page via ViewBag "/Reservation/ReservationView"
            ViewBag.mainList = mainList;
            return View();
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
                guestDetail.Add((guest.FirstNameDetails() + " "+guest.LastNameDetails()),guest.GuestIdDetails());
            }

            // storing view Form data type to show on View Form
            resTemp.Add("Guest Name",default(string));
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
            
            foreach (var guest in guestList)
            {
                guestDetail.Add((guest.FirstNameDetails() + " " + guest.LastNameDetails()), guest.GuestIdDetails());
            }

            Dictionary<string, object> resTemp = new Dictionary<string, object>();

            // Add all POST data into a dictionary
            int guestID = guestDetail[Request.Form["Guest Name"]];
            resTemp.Add("guestID", guestID);
            resTemp.Add("numOfGuest", Convert.ToInt32(Request.Form["Number of Guests"].ToString()));
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

            // After completion of creation to redirect user to "/Reservation/ReservationView"
            return RedirectToAction("ReservationView");
        }

    }
}
