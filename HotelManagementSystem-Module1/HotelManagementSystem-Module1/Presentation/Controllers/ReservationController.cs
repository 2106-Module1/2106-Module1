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
            Array[] mainList = { };
            IEnumerable<Guest> guestList = new List<Guest>();
            // This will return back to the view 
            // May require to changes once view layout/design is out
            
            /*guestList.Add("10000003", "Wong Ah Kow");
            guestList.Add("10000010", "Kendrick Wee");
            ViewBag.guestList = guestList;*/
            
            foreach (var res in reservationList)
            {
                Guest g = _guestService.SearchByGuestId((int) res.GetReservation()["resID"]);
                if (g != null)
                {
                    String[] subList =
                    {
                        (string)res.GetReservation()["resID"],
                        (string)res.GetReservation()["guestID"],
                        g.FirstNameDetails(),
                        g.EmailDetails(),
                        (string)res.GetReservation()["numOfGuest"],
                        (string)res.GetReservation()["roomType"],
                        (string)res.GetReservation()["start"],
                        (string)res.GetReservation()["end"],
                        (string)res.GetReservation()["remark"],
                        (string)res.GetReservation()["modified"],
                        (string)res.GetReservation()["promoCode"],
                        (string)res.GetReservation()["price"],
                        (string)res.GetReservation()["status"]
                    };
                    mainList.Append(subList);
                }
            }

            ViewBag.mainList = mainList;

            /*ViewBag.guestList = guestList;
            ViewBag.reservationList = reservationList;*/
            return View();
        }

        [HttpGet]
        public IActionResult CreateReservation()
        {
            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            string[] resFields = { "Number of Guests", "Room Type", "Check-In Date/Time", "Check-Out Date/Time", "Remarks", "Promotion Code", "Price" };

            IEnumerable<Guest> guestList = _guestService.RetrieveGuests();
            Dictionary<int, string> guestDetail = new Dictionary<int, string>();

            foreach (var guest in guestList)
            {
                guestDetail.Add(guest.GuestIdDetails(), (guest.FirstNameDetails() + guest.LastNameDetails()));
            }

            resTemp.Add("Guest Name",default(string));
            resTemp.Add("Number of Guests", default(int));
            resTemp.Add("Room Type", default(string));
            resTemp.Add("Check-In Date/Time", DateTime.Now.Date.AddHours(10));
            resTemp.Add("Check-Out Date/Time", DateTime.Now.Date.AddHours(14));
            resTemp.Add("Remarks", default(string));
            resTemp.Add("Promotion Code", default(string));
            resTemp.Add("Price", default(double));

            ViewBag.reservationTemp = resTemp;
            ViewBag.guestDetail = guestDetail;
            return View(resTemp);
        }

        [HttpPost]
        public IActionResult CreateReservation(Dictionary<string, object> newReservation)
        {
            ViewData["form"]="post";
            Dictionary<string, string> guestList = new Dictionary<string, string>();
            guestList.Add("Wong Ah Kow","10000003");
            guestList.Add("Kendrick Wee", "10000010");

            string guestID = guestList[Request.Form["Guest Name"]];

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
            
            resTemp.Add("guestID", Convert.ToInt32(guestID.ToString()));
            resTemp.Add("numOfGuest", Convert.ToInt32(Request.Form["Number of Guests"].ToString()));
            resTemp.Add("roomType", Request.Form["Room Type"].ToString());
            resTemp.Add("start", Convert.ToDateTime(Request.Form["Check-In Date/Time"].ToString()));
            resTemp.Add("end", Convert.ToDateTime(Request.Form["Check-Out Date/Time"].ToString()));
            resTemp.Add("remark", Request.Form["Remarks"].ToString());
            resTemp.Add("modified", DateTime.Now);
            resTemp.Add("promoCode", Request.Form["Promotion Code"].ToString());
            resTemp.Add("price", Convert.ToDouble(Request.Form["Price"].ToString()));
            resTemp.Add("status", "Not Fulfilled");
            
            // ViewBag.reservationTemp = resTemp;
            Reservation createdReservation = (Reservation)new Reservation().SetReservation(resTemp);
            _reservationService.CreateReservation(createdReservation);

            // Dictionary<string, object> resTempobj = createdReservation.GetReservation();
            
            return RedirectToAction("ReservationView");
        }

    }
}
