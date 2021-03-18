using System;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using System.Xml;
using System.Diagnostics;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace HotelManagementSystem.Domain
{
    public class Authenticate : IAuthenticate
    {
        private readonly IAuthenticateRepository authRepo;
        private readonly IPinRepository iPinRepo;
        private readonly IStaffGateway staffGateway;
        public Authenticate(IAuthenticateRepository authrep, IPinRepository pinrepo, IStaffGateway gateway)
        {
            authRepo = authrep;
            iPinRepo = pinrepo;
            staffGateway = gateway;
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

        /// <summary>
        /// This functions is validate managers pin
        /// </summary>
        /// <returns>bool</returns>
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

        public bool AuthenticateLogin(string staff_user, string staff_password)
        {


            //IEnumerable<Staff> staffList;

            byte[] bytes = Encoding.Unicode.GetBytes(staff_password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            //staffList = staffGateway.RetrieveStaffDetailsByRole("Manager");

            //foreach(var s in staffList)
            //{
            //    Debug.WriteLine(s.StaffEmailDetail());
            //}

            /// <summary>
            /// Authenticate and get staff object
            /// 
            /// If(CheckPinExpiry()) {
            ///     var genPin = GeneratePin();
            ///     iPinRepo.UpdatePin(new Pin(1, genPin));
            ///     pinSrv.ChangePinState(false);
            ///     SendEmail(staff.email, genPin)
            /// }
            /// do nothing if pin not expired.
            /// 
            /// </summary>
            /// 

            return true;
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