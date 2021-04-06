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
        /// This functions is to validate managers'pin
        /// </summary>
        /// <param name="pin"></param>
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
        /// <summary>
        /// Check and validate login based on username and password
        /// </summary>
        /// <param name="staff_user"></param>
        /// <param name="staff_password"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// This functions is to validate managers'pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns>bool</returns>
        public bool AuthenticatePin(string pin)
        {
            return ValidatePin(pin);
        }
        /// <summary>
        /// Retrieve staff based on username
        /// </summary>
        /// <param name="staff_user"></param>
        /// <returns>Staff entity</returns>
        public Staff RetrieveStaff(string staff_user)
        {
            return staffGateway.RetreieveStaffDetails(staff_user);
        }
    }
}