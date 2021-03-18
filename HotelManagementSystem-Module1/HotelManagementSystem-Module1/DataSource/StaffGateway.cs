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

        public void InsertStaff(Staff staff)
        {
            if (staff != null)
            {
                appDbContext.StaffDb().Add(staff);
                appDbContext.SaveChanges();
            }
        }

        public IEnumerable<Staff> RetrieveStaffDetailsByRole(string role)
        {
            return appDbContext.StaffDb().AsEnumerable().Where(entity => (entity.StaffRoleDetail() == role));
        }
    }
}
