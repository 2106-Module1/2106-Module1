using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IStaffGateway
    {
        /// <summary>
        /// Return a Staff entity based on username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Staff</returns>
        Staff RetreieveStaffDetails(string username);

        /// <summary>
        /// Retrieve a collection of Staff entities based on role
        /// </summary>
        /// <param name="role">user role</param>
        /// <returns>IEnumerable Collection of Staff entities</returns>
        IEnumerable<Staff> RetrieveStaffDetailsByRole(string role);

    }
}
