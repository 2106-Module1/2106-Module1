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
using System.Text;

namespace HotelManagementSystem.Presentation.Controllers
{
    public class AuthenticateController : Controller
    {

        private readonly IAuthenticate auth;
        private readonly IPinRepository iPinRepo;
        private ITimerService pinSrv = new TimerService();
        public AuthenticateController(IAuthenticate authenticator, IPinRepository pinRepo)
        {
            auth = authenticator;
            iPinRepo = pinRepo;
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
                pinSrv.changePinState(false);
                var genPin = GeneratePin();
                iPinRepo.UpdatePin(new Pin(1,genPin));

            }
            return View();
        }

        public string GeneratePin()
        {
            Random _random = new Random();
            var random_digit = 0;
            random_digit = _random.Next(1000, 9999);

            var pinBuilder = new StringBuilder();
            pinBuilder.Append(random_digit);
            return pinBuilder.ToString();
        }

        public void sendEmail()
        {
            throw new NotImplementedException();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
