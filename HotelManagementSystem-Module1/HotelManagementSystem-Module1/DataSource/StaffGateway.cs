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

        
        public Staff RetreieveStaffDetails(string username)
        {
           return appDbContext.StaffDb().AsEnumerable().SingleOrDefault(entity => entity.StaffUsernameDetail() == username );
        }


        public IEnumerable<Staff> RetrieveStaffDetailsByRole(string role)
        {
            return appDbContext.StaffDb().AsEnumerable().Where(entity => (entity.StaffRoleDetail() == role));
        }
    }
}
