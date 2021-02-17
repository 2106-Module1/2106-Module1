using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.DataSource;
using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class CreateReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public CreateReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
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

            Reservation createdReservation = new Reservation();

            createdReservation.CreateReservation(resTemp);

            Dictionary<string, object> resTempobj = createdReservation.GetReservation();

            ViewData["value"] = resTempobj["numOfGuest"];

            return View();
        }

    }
}
