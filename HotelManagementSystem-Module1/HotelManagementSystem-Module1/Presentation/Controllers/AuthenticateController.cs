using HotelManagementSystem.Models;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using HotelManagementSystem.Domain;
using Microsoft.AspNetCore.Http;

namespace HotelManagementSystem.Presentation.Controllers
{
    public class AuthenticateController : Controller
    {
        public const string SessionKeyUser = "_Name";
        public const string SessionKeyID = "_ID";
        public const string SessionKeyRole = "_Role";
        private IAuthenticate auth;

        public string SessionName { get; private set; }

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

            string username = "";
            string password = "";

            username = Request.Form["txtUser"].ToString();
            password = Request.Form["txtPassword"].ToString();

            bool isLogin = auth.AuthenticateLogin(username, password);
            if (isLogin)
            {
                Staff retrievedStaff = auth.RetrieveStaff(username);

                string user = retrievedStaff.StaffUsernameDetail();
                int id = retrievedStaff.StaffIDDetail();
                string role = retrievedStaff.StaffRoleDetail(); 

                HttpContext.Session.SetString(SessionKeyUser, user);
                HttpContext.Session.SetInt32(SessionKeyID, id);
                HttpContext.Session.SetString(SessionKeyRole, role);

                
                return View("~/Views/Shared/Home.cshtml");
            }
            else
            {
                return View("Login");
            }
            

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
