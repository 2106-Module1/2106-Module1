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
        
        private readonly IAuthenticate auth;
        public AuthenticateController(IAuthenticate authenticator)
        {
            auth = authenticator;
        }

        public IActionResult ValidatePin()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
