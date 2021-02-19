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
        private readonly IAuthenticate staffGateway;
        public AuthenticateController(IAuthenticate authenticater, IAuthenticate staffGateway, IAuthenticateGateway inAuthenticateGateway, IStaffGateway inStaffGateway)
        {
            authenticate = authenticater;
            //staffGateway = inStaffGateway;
        }
        public bool AuthenticateLogin()
        {


            IEnumerable<Staff> staff;
            //string pass = staff.RetrieveStaff;
            return true;
        }

        public string generatePin()
        {

            return "0000";
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
