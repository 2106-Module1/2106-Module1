using HotelManagementSystem.Models;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using HotelManagementSystem.Domain;


namespace HotelManagementSystem.Presentation.Controllers
{
    public class AuthenticateController : Controller
    {

        private IAuthenticate auth;
        
        public AuthenticateController(IAuthenticate authenticator)
        {
            auth = authenticator;

        }

        public IActionResult ViewLogin()
        {

            return View("Login");

        }


        public IActionResult ValidatePin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            //string username = "";
            //string password = "";

            //username = Request.Form["txtUser"].ToString();
            //password = Request.Form["txtPassword"].ToString();

            //bool isLogin = auth.AuthenticateLogin(username,password);
            //if (isLogin)
            //{
            //    return View("~/Views/Home/Index.cshtml");
            //}
            //else
            //{
            //    return View("Login", "Invalid Username or password");
            //}
            return View();

        }

      


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
