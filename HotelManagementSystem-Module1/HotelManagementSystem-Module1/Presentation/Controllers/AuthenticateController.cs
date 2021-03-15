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
using MimeKit;
using System.Xml;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace HotelManagementSystem.Presentation.Controllers
{
    public class AuthenticateController : Controller
    {

        //private readonly IAuthenticate auth;
        private readonly IPinRepository iPinRepo;
        private readonly IAuthenticateRepository iAuthGateway;
        private readonly ITimerService pinSrv = new TimerService();
        public AuthenticateController(IPinRepository pinRepo,IAuthenticateRepository authGateway)
        {
            //auth = authenticator;
            iPinRepo = pinRepo;
            iAuthGateway = authGateway;

        }

        public IActionResult ViewLogin()
        {



            return View("Login");

        }


        public IActionResult ValidatePin()
        {
            return View();
        }

        public IActionResult ViewPin()
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

            bool isLogin = iAuthGateway.validateLogin(username,password);
            if(isLogin)
            {
                return View("~/Views/Home/Index.cshtml");
            }            else
            {
                return View("Login", "Invalid Username or password");
            }

            //Check if pin is expired if its false means not expired so don't generate pin yet
            var pinState = pinSrv.CheckPinExpired();
            if(pinState == true)
            {
                var genPin = GeneratePin();
                iPinRepo.UpdatePin(new Pin(1,genPin));
                //need to get staffEmail
                SendEmail("", genPin);
                pinSrv.ChangePinState(false);
            } 
            return View();
        }

        /// <summary>
        /// Generate 4 digit pin
        /// </summary>
        /// <returns>4 digit pin number</returns>
        public string GeneratePin()
        {
            Random _random = new Random();
            var random_digit = 0;
            random_digit = _random.Next(1000, 9999);

            var pinBuilder = new StringBuilder();
            pinBuilder.Append(random_digit);
            return pinBuilder.ToString();
        }

        /// <summary>
        /// Sends email to staff when new pin is generated
        /// </summary>
        public void SendEmail(string staffEmail, string pin)
        {
            //Uses XML reader to read the mail template
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            var mailBody = "";
            var mailAddFrom = "";
            var mailPw = "";
            var mailSubj = "";
            
            XmlReader xReader = XmlReader.Create("Properties/MailTemplate.xml", settings);
            while (xReader.Read())
            {
                switch (xReader.Name)
                {
                    case "EmailSubject":
                        mailSubj = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        break;
                    case "EmailBody":
                        mailBody = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        break;
                    case "EmailAdressFrom":
                        mailAddFrom = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        Debug.WriteLine(mailAddFrom);
                        break;
                    case "EmailPassword":
                        mailPw = xReader.ReadElementContentAs(typeof(string), null).ToString().Trim();
                        break;
                    default:
                        break;
                }
            }
            if (!staffEmail.Equals(""))
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(mailAddFrom, "No-Reply@praefor2105@gmail.com");
                mailMessage.To.Add(staffEmail);
                mailMessage.Subject = mailSubj;
                mailMessage.Body = mailBody;

                string message = string.Format(mailMessage.Body, pin);
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message;

                using var smtpClient = new SmtpClient();
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate(mailAddFrom, mailPw);
                smtpClient.Send((MimeMessage)mailMessage);
                smtpClient.Disconnect(true);
            }

        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
