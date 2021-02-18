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
            IEnumerable<Reservation> reservationList = _reservationService.GetAllReservations();
            IEnumerable<Guest> guestList = new List<Guest>();
            ArrayList mainList = new ArrayList();
            
            foreach (var res in reservationList)
            {
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

            ViewBag.mainList = mainList;
            return View();
        }
        
        [HttpGet]
        public IActionResult CreateReservation()
        {
            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            string[] resFields = { "Number of Guests", "Room Type", "Check-In Date/Time", "Check-Out Date/Time", "Remarks", "Promotion Code", "Price" };

            IEnumerable<Guest> guestList = _guestService.RetrieveGuests();
            Dictionary<string, int> guestDetail = new Dictionary<string, int>();
            List<string> guestName = new List<string>();

            foreach (var guest in guestList)
            {
                guestName.Add(guest.FirstNameDetails() + " " + guest.LastNameDetails());
                guestDetail.Add((guest.FirstNameDetails() + " "+guest.LastNameDetails()),guest.GuestIdDetails());
            }

            resTemp.Add("Guest Name",default(string));
            resTemp.Add("Number of Guests", default(int));
            resTemp.Add("Room Type", default(string));
            resTemp.Add("Check-In Date/Time", DateTime.Now.Date.AddHours(10));
            resTemp.Add("Check-Out Date/Time", DateTime.Now.Date.AddHours(14));
            resTemp.Add("Remarks", default(string));
            resTemp.Add("Promotion Code", default(string));
            resTemp.Add("Price", default(double));

            ViewBag.guestName = guestName;
            ViewBag.reservationTemp = resTemp;
            ViewBag.guestDetail = guestDetail;
            return View(resTemp);
        }

        [HttpPost]
        public IActionResult CreateReservation(Dictionary<string, object> newReservation)
        {
            ViewData["form"]="post";
            IEnumerable<Guest> guestList = _guestService.RetrieveGuests();
            Dictionary<string, int> guestDetail = new Dictionary<string, int>();
            
            foreach (var guest in guestList)
            {
                
                guestDetail.Add((guest.FirstNameDetails() + " " + guest.LastNameDetails()), guest.GuestIdDetails());
            }
            
            int guestID = guestDetail[Request.Form["Guest Name"]];

            Dictionary<string, object> resPostForm = new Dictionary<string, object>();
            resPostForm.Add("Guest Name", default(string));
            resPostForm.Add("Number of Guests", default(int));
            resPostForm.Add("Room Type", default(string));
            resPostForm.Add("Check-In Date/Time", DateTime.Now.Date.AddHours(10));
            resPostForm.Add("Check-Out Date/Time", DateTime.Now.Date.AddHours(14));
            resPostForm.Add("Remarks", default(string));
            resPostForm.Add("Promotion Code", default(string));
            resPostForm.Add("Price", default(double));
            ViewBag.reservationTemp = resPostForm;

            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            
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
            
            Reservation createdReservation = (Reservation)new Reservation().SetReservation(resTemp);
            _reservationService.CreateReservation(createdReservation);
            
            return RedirectToAction("ReservationView");
        }

    }
}
