using System;

using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using System.Xml;
using System.Diagnostics;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Text;

namespace HotelManagementSystem.Domain
{
    public class Authenticate : IAuthenticate
    {
        private readonly IAuthenticateRepository authRepo;
        private readonly IPinRepository iPinRepo;
        private readonly ITimerService timerSrv = new TimerService();

        public Authenticate(IAuthenticateRepository authRep, IPinRepository pinRepo)
        {
            iPinRepo = pinRepo;
            authRepo = authRep;

        }
        private bool ComparePass(string username, string password)
        {
            string pass = authRepo.CheckPass(username);
            if (pass != null)
            {
                return pass == password;
            }
            return false;
        }

        private bool ValidatePin(string pin)
        {
            var pinObj = iPinRepo.ValidatePin(pin);
            if (pinObj != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Generate 4 digit pin
        /// </summary>
        /// <returns>4 digit pin number</returns>
        private string GeneratePin()
        {
            Random _random = new Random();
            var random_digit = 0;
            random_digit = _random.Next(1000, 9999);

            var pinBuilder = new StringBuilder();
            pinBuilder.Append(random_digit);
            return pinBuilder.ToString();
        }

        private bool CheckPinExpiry()
        {
            var pinState = timerSrv.CheckPinExpired();
            return pinState;
        }

        /// <summary>
        /// Sends email to staff when new pin is generated
        /// </summary>

        private void SendEmail(string staffEmail, string pin)
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

        public bool CheckPinExpired()
        {
            return CheckPinExpiry();
        }

        public Staff AuthenticateLogin(string staff_user, string staff_password)
        {
            /// <summary>
            /// Authenticate and get staff object
            /// 
            /// If(CheckPinExpired()) {
            ///     var genPin = GeneratePin();
            ///     iPinRepo.UpdatePin(new Pin(1, genPin));
            ///     pinSrv.ChangePinState(false);
            ///     SendEmail(staff.email, genPin)
            /// }
            /// do nothing if pin not expired.
            /// 
            /// </summary>

            throw new NotImplementedException();
        }

        public bool AuthenticatePin(string pin)
        {
            return ValidatePin(pin);
        }

        public Staff RetrieveStaff()
        {
            throw new NotImplementedException();
        }
    }
}