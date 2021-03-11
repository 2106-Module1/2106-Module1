using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections;

/*
 * Owner of ReservationController: Mod 1 Team 4
 * This Controller is used for Viewing Records only.
 */
namespace HotelManagementSystem.Presentation.Controllers
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
         * Function to retrieve all existing reservations found in the database / or
         * View all existing (Past, Current, Future) reservation of a guest found in the database
         * </summary>
         */
        [HttpGet]
        public IActionResult ReservationView()
        {
            bool hasKeys = Request.QueryString.HasValue;
            if (hasKeys)
            {
                // Initializing ArrayList to store all existing reservation data to pass to View Page
                ArrayList mainList = new ArrayList();

                // Retrieving required data for the function to work
                int guestId = Convert.ToInt32(Request.Query["GuestId"]);
                Guest g = _guestService.SearchByGuestId(guestId);
                IEnumerable<Reservation> individualGuestReservationList = _reservationService.SearchByGuestId(guestId);

                // For loop to create a array of reservation data from the database
                foreach (var res in individualGuestReservationList)
                {
                    // Retrieving Reservation Object Data and storing into a Dictionary<string, object>
                    Dictionary<string, object> reservation = res.GetReservation();

                    // Storing Reservation Object Data into subList String Array
                    string[] subList =
                    {
                        reservation["resID"].ToString(),
                        reservation["numOfGuest"].ToString(),
                        reservation["roomType"].ToString(),
                        reservation["start"].ToString(),
                        reservation["end"].ToString(),
                        reservation["status"].ToString()
                    };

                    // Storing subList into mainList (ArrayList)
                    mainList.Add(subList);
                }

                // List of data stored in the ViewBag to be passed to View
                ViewBag.flag = 0;
                ViewBag.mainList = mainList;
                ViewBag.GuestId = guestId;
                ViewBag.GuestName = g.FirstNameDetails() + " " + g.LastNameDetails();
                ViewBag.GuestEmail = g.EmailDetails();
                ViewBag.GuestType = g.GuestTypeDetails();
                ViewBag.GuestPassport = g.PassportNumberDetails();
            }
            else
            {
                // Initializing ArrayList to store all existing reservation data to pass to View Page
                ArrayList mainList = new ArrayList();

                // Retrieving all existing reservation inside the database
                IEnumerable<Reservation> reservationList = _reservationService.GetAllReservations();

                // For Loop to store all existing reservation with guest name and email into a ArrayList to pass to View page 
                foreach (var res in reservationList)
                {
                    // Retrieving guest by id based on Mod 1 Team 9 function
                    Guest g = _guestService.SearchByGuestId((int)res.GetReservation()["guestID"]);

                    if (g != null)
                    {
                        Dictionary<string, object> reservation = res.GetReservation();
                        string[] subList =
                        {
                            reservation["resID"].ToString(),
                            reservation["guestID"].ToString(),
                            g.FirstNameDetails() + " " + g.LastNameDetails(),
                            g.EmailDetails(),
                            reservation["numOfGuest"].ToString(),
                            reservation["roomType"].ToString(),
                            reservation["start"].ToString(),
                            reservation["end"].ToString(),
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
    }
}
