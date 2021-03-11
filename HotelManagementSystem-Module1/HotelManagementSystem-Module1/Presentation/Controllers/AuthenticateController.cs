using HotelManagementSystem.Models;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain;

namespace HotelManagementSystem.Presentation.Controllers
{
    public class AuthenticateController : Controller
    {

        private readonly IAuthenticate auth;
        private readonly Authenticate authObj;
        private IPinService pinSrv = new PinService();
        public AuthenticateController(IAuthenticate authenticator)
        {
            auth = authenticator;
        }

        public IActionResult ValidatePin()
        {
            return View();
        }

        public IActionResult ViewPin()
        {
            return View();
        }


        public IActionResult Login()
        {
            //Check for pinState if its false means not expired so don't genenratePin yet
            var pinState = pinSrv.checkPinState();
            if(pinState == true)
            {
               
            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
