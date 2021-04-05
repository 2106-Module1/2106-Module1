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


        /// <summary>
        /// This functions is validate managers pin
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidatePin(string pin)
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
            byte[] bytes = Encoding.Unicode.GetBytes(staff_password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            bool check = authRepo.ValidateLogin(staff_user, hashString);
            
            return check;
        }

        public bool AuthenticatePin(string pin)
        {
            return ValidatePin(pin);
        }

        public Staff RetrieveStaff(string staff_user)
        {
            return staffGateway.RetreieveStaffDetails(staff_user);
        }
    }
}