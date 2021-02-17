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


            resTemp.Add("numOfGuest", default(int));
            resTemp.Add("roomType", default(string));
            resTemp.Add("start", default(DateTime));
            resTemp.Add("end", default(DateTime));
            resTemp.Add("remark", default(string));
            resTemp.Add("modified", default(DateTime));
            resTemp.Add("promoCode", default(string));
            resTemp.Add("price", default(double));
            resTemp.Add("status", default(string));
            ViewData["value"] = "hello";
            ViewBag.reservationTemp = resTemp;
            return View(resTemp);
            
        }

        [HttpPost]
        public IActionResult Index(Dictionary<string, object> newReservation)
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
