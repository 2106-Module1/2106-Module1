using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.DataSource;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.Domain.Models;

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
            // This will return back to the view 
            // May require to changes once view layout/design is out
            return View();
        }

        [HttpGet]
        public IActionResult CreateReservation()
        {
            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            string[] resFields = { "Number of Guests", "Room Type", "Check-In Date/Time", "Check-Out Date/Time", "Remarks", "Promotion Code", "Price" };
            // IEnumerable<Guest> guestList = _guestService.RetrieveGuests();
            
            resTemp.Add("Number of Guests", default(int));
            resTemp.Add("Room Type", default(string));
            resTemp.Add("Check-In Date/Time", DateTime.Now.Date.AddHours(10));
            resTemp.Add("Check-Out Date/Time", DateTime.Now.Date.AddHours(14));
            resTemp.Add("Remarks", default(string));
            resTemp.Add("Promotion Code", default(string));
            resTemp.Add("Price", default(double));

            ViewData["value"] = "hello";
            ViewBag.reservationTemp = resTemp;
            return View(resTemp);
        }

        [HttpPost]
        public IActionResult CreateReservation(Dictionary<string, object> newReservation)
        {

            Dictionary<string, object> resTemp = new Dictionary<string, object>();


            resTemp.Add("numOfGuest", Convert.ToInt32(Request.Form["Number of Guests"].ToString()));
            resTemp.Add("roomType", Request.Form["Room Type"].ToString());
            resTemp.Add("start", Convert.ToDateTime(Request.Form["Check-In Date/Time"].ToString()));
            resTemp.Add("end", Convert.ToDateTime(Request.Form["Check-Out Date/Time"].ToString()));
            resTemp.Add("remark", Request.Form["Remarks"].ToString());
            resTemp.Add("modified", DateTime.Now);
            resTemp.Add("promoCode", Request.Form["Promotion Code"].ToString());
            resTemp.Add("price", Convert.ToDouble(Request.Form["Price"].ToString()));
            resTemp.Add("status", "Not Fulfilled");

            ViewBag.reservationTemp = resTemp;
            Reservation createdReservation = (Reservation)new Reservation().SetReservation(resTemp);
            _reservationService.CreateReservation(createdReservation);

            Dictionary<string, object> resTempobj = createdReservation.GetReservation();

            ViewData["value"] = resTempobj["numOfGuest"];

            return View();
        }

    }
}
