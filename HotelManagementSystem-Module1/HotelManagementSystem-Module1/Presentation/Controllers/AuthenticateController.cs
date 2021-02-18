using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.DataSource;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;

namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticate authenticate;
        public AuthenticateController(IAuthenticate authenticater)
        {
            authenticate = authenticater;
        }
        public bool AuthenticateLogin()
        {


            IEnumerable<Staff> staff;
            //string pass = RetrievePass;
            return true;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
