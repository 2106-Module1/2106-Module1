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

        


        public IEnumerable<Staff> RetreieveStaffDetails()
        {
           return appDbContext.StaffDb().AsEnumerable();
        }

        public void UpdateStaffDetails(Staff modifiedStaff)
        {
            throw new NotImplementedException();
        }


    }
}
