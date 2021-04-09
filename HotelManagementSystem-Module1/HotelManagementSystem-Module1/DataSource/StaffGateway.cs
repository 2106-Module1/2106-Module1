using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public class StaffGateway : IStaffGateway
    {
        private readonly IAppDbContext appDbContext;

        public StaffGateway(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }

        /// <summary>
        /// Return a Staff entity based on username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Staff</returns>
        public Staff RetreieveStaffDetails(string username)
        {
           return appDbContext.StaffDb().AsEnumerable().SingleOrDefault(entity => entity.StaffUsernameDetail() == username );
        }

        /// <summary>
        /// Retrieve a collection of Staff entities based on role
        /// </summary>
        /// <param name="role">user role</param>
        /// <returns>IEnumerable Collection of Staff entities</returns>
        public IEnumerable<Staff> RetrieveStaffDetailsByRole(string role)
        {
            return appDbContext.StaffDb().AsEnumerable().Where(entity => (entity.StaffRoleDetail() == role));
        }
    }
}
