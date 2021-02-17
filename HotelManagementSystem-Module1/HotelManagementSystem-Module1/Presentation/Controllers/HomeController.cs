using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.Domain.Models;
using Newtonsoft.Json;
using System.Collections;

namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            Dictionary<string, object> resTemp = new Dictionary<string, object>();
            string[] resFields = { "Number of Guests", "Room Type", "Check-In Date/Time", "Check-Out Date/Time", "Remarks", "Promotion Code", "Price" };
            
            


            resTemp.Add("Number of Guests", default(int));
            resTemp.Add("Room Type", default(string));
            resTemp.Add("Check-In Date/Time", DateTime.Now.Date.AddHours(10));
            resTemp.Add("Check-Out Date/Time", DateTime.Now.Date.AddHours(14));
            resTemp.Add("Remarks", default(string));
            resTemp.Add("Promotion Code", default(string));
            resTemp.Add("Price","");

            ViewData["value"] = "hello";
            ViewBag.reservationTemp = resTemp;
            return View(resTemp);
            
        }

        [HttpPost]
        public IActionResult Index(Dictionary<string, object> newReservation)
        {

            Dictionary<string, object> resTemp = new Dictionary<string, object>();


            resTemp.Add("numOfGuest", Convert.ToInt32(Request.Form["Number of Guests"].ToString()));
            resTemp.Add("roomType", Request.Form["Room Type"].ToString());
            resTemp.Add("start", Convert.ToDateTime(Request.Form["Check-In Date/Time"].ToString()));
            resTemp.Add("end", Convert.ToDateTime(Request.Form["Check-Out Date/Time"].ToString()));
            resTemp.Add("remark", Request.Form["Remarks"].ToString());
            resTemp.Add("modified",DateTime.Now);
            resTemp.Add("promoCode", Request.Form["Promotion Code"].ToString());
            resTemp.Add("price", Convert.ToDouble(Request.Form["Price"].ToString()));
            resTemp.Add("status", "Not Fulfilled");



            ViewBag.reservationTemp = resTemp;

           
            Reservation createdReservation = new Reservation();

            createdReservation.CreateReservation(resTemp);

            Dictionary<string, object> resTempobj = createdReservation.GetReservation();

            ViewData["form"] = "post";
            ViewData["value"] = resTempobj;




            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
