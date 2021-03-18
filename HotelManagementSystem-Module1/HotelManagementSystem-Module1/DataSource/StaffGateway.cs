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

        
        public Staff RetreieveStaffDetails(string username , string password)
        {
           return appDbContext.StaffDb().SingleOrDefault(entity => entity.StaffUsernameDetail() == username && entity.StaffPasswordDetail() == password);
        }


        public void UpdateStaffDetails(Staff modifiedStaff)
        {
            throw new NotImplementedException();
        }

        public void InsertStaff(Staff entity)
        {
            if (entity != null)
            {
                appDbContext.StaffDb().Update(entity);
                appDbContext.SaveChanges();
            }
        }
    }
}
