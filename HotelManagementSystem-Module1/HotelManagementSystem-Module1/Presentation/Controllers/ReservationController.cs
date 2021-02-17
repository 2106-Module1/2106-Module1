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

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        public IActionResult Index()
        {
            // This will return back to the view 
            // May require to changes once view layout/design is out
            return View();
        }

        [HttpGet]
        public IActionResult CreateReservation()
        {
            // This will return back to the view 
            // May require to changes once view layout/design is out
            return View();
        }

        [HttpPost]
        public IActionResult CreateReservation(Dictionary<string, object> newReservation)
        {

            Dictionary<string, object> resTemp = new Dictionary<string, object>();


            resTemp.Add("numOfGuest", Convert.ToInt32(Request.Form["numOfGuest"].ToString()));
            resTemp.Add("roomType", Request.Form["roomType"].ToString());
            resTemp.Add("start", Convert.ToDateTime(Request.Form["start"].ToString()));
            resTemp.Add("end", Convert.ToDateTime(Request.Form["end"].ToString()));
            resTemp.Add("remark", Request.Form["remark"].ToString());
            resTemp.Add("modified", Convert.ToDateTime(Request.Form["modified"].ToString()));
            resTemp.Add("promoCode", Request.Form["promoCode"].ToString());
            resTemp.Add("price", Convert.ToDouble(Request.Form["price"].ToString()));
            resTemp.Add("status", Request.Form["status"].ToString());

            ViewBag.reservationTemp = resTemp;
            Reservation createdReservation = new Reservation(resTemp);
            _reservationService.CreateReservation(createdReservation);

            createdReservation.SetReservation(resTemp);

            _reservationService.CreateReservation(createdReservation);

            Dictionary<string, object> resTempobj = createdReservation.GetReservation();

            ViewData["value"] = resTempobj["numOfGuest"];

            return View();
        }

    }
}
