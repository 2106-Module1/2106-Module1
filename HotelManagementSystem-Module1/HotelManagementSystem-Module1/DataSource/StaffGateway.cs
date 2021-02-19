using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public class StaffGateway : IStaffGateway
    {
        private readonly IAppDbContext appDbContext;

        public StaffGateway(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }


        public Staff getPassword(int staff_id)
        {
            return appDbContext.StaffDb().Where(entity => entity.StaffIDDetail() == staff_id).SingleOrDefault();
            throw new NotImplementedException();
        }
    }
}
