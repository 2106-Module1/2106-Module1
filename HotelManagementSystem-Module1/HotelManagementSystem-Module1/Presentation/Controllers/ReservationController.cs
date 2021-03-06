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
 * This Controller is used for Viewing Records only.
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

        /*
         * <summary>
         * (Completed)
         * Function to retrieve all existing reservations
         * </summary>
         */
        [HttpGet]
        public IActionResult ReservationView()
        {
            bool hasKeys = Request.QueryString.HasValue;
            if (hasKeys)
            {
                int guestId = Convert.ToInt32(Request.Query["GuestId"]);
                ArrayList mainList = new ArrayList();
                Guest g = _guestService.SearchByGuestId(guestId);
                IEnumerable<Reservation> individualGuestReservationList = _reservationService.SearchByGuestId(guestId);

                foreach (var res in individualGuestReservationList)
                {
                    // Retrieving guest by id based on Mod 1 Team 9 function
                    Dictionary<string, object> reservation = res.GetReservation();
                    string[] subList =
                    {
                        reservation["resID"].ToString(),
                        reservation["numOfGuest"].ToString(),
                        reservation["roomType"].ToString(),
                        reservation["start"].ToString(),
                        reservation["end"].ToString(),
                        reservation["modified"].ToString(),
                        reservation["status"].ToString()
                    };
                    mainList.Add(subList);
                }

                ViewBag.flag = 0;
                ViewBag.mainList = mainList;
                ViewBag.GuestName = g.FirstNameDetails() + " " + g.LastNameDetails();
                ViewBag.GuestEmail = g.EmailDetails();
                ViewBag.GuestType = g.GuestTypeDetails();
                ViewBag.GuestPassport = g.PassportNumberDetails();
            }
            else
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
                        Dictionary<string, object> reservation = res.GetReservation();
                        String[] subList =
                        {
                            reservation["resID"].ToString(),
                            reservation["guestID"].ToString(),
                            g.FirstNameDetails() + " " + g.LastNameDetails(),
                            g.EmailDetails(),
                            reservation["numOfGuest"].ToString(),
                            reservation["roomType"].ToString(),
                            reservation["start"].ToString(),
                            reservation["end"].ToString(),
                            /*reservation["remark"].ToString(),
                            reservation["modified"].ToString(),
                            reservation["promoCode"].ToString(),
                            reservation["price"].ToString(),*/
                            reservation["status"].ToString()
                        };
                        mainList.Add(subList);
                    }
                }

                // Passing data over to View Page via ViewBag "/Reservation/ReservationView"
                ViewBag.flag = 1;
                ViewBag.mainList = mainList;
                
            }
            return View();
        }

        /*
         * <summary>
         * Function to retrieve all the reservations for a particular guest
         * TODO: for Deliverable 3
         * </summary>
         */
        /*[HttpGet]
        public IActionResult GuestReservationRecord()
        {
            int guestId = Convert.ToInt32(Request.Query["GuestId"]);
            ArrayList mainList = new ArrayList();
            Guest g = _guestService.SearchByGuestId(guestId);
            IEnumerable<Reservation> individualGuestReservationList = _reservationService.SearchByGuestId(guestId);

            foreach (var res in individualGuestReservationList)
            {
                // Retrieving guest by id based on Mod 1 Team 9 function
                Dictionary<string, object> reservation = res.GetReservation();
                string[] subList =
                {
                    reservation["resID"].ToString(),
                    reservation["numOfGuest"].ToString(),
                    reservation["roomType"].ToString(),
                    reservation["start"].ToString(),
                    reservation["end"].ToString(),
                    reservation["modified"].ToString(),
                    reservation["status"].ToString()
                };
                mainList.Add(subList);
            }

            ViewBag.mainList = mainList;
            ViewBag.GuestName = g.FirstNameDetails() + " " + g.LastNameDetails();
            ViewBag.GuestEmail = g.EmailDetails();
            return View();
        }*/
    }
}
