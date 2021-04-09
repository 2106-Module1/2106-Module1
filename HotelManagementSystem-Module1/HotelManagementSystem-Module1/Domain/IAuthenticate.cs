using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HotelManagementSystem.Domain
{
    public interface IAuthenticate
    {
        /// <summary>
        /// Check and validate login based on username and password
        /// </summary>
        /// <param name="staff_user"></param>
        /// <param name="staff_password"></param>
        /// <returns>bool</returns>
        bool AuthenticateLogin(string staff_user, string staff_password);

        /// <summary>
        /// This functions is to validate managers'pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns>bool</returns>
        bool AuthenticatePin(string pin);

        /// <summary>
        /// Retrieve staff based on username
        /// </summary>
        /// <param name="staff_user"></param>
        /// <returns>Staff entity</returns>
        Staff RetrieveStaff(string staff_user);
    }
}
