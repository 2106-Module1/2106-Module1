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

        /*
         * <summary>
         * Function to retrieve all the reservations for a particular guest
         * TODO: for Deliverable 3
         * </summary>
         */
        public IActionResult ViewAllGuestRecord()
        {
            //TODO : retrieve all the reservations for a particular guest
            throw new NotImplementedException();
        }

        /*
         * <summary>
         * Function to retrieve a single Reservation Record to display in detail.
         * This function will link to Update Reservation if there is a need to update.
         * TODO: for Deliverable 3
         * </summary>
         */
        public IActionResult ViewReservationRecord()
        {
            //TODO : check database for clashing reservation timings
            throw new NotImplementedException();
        }

    }
}
