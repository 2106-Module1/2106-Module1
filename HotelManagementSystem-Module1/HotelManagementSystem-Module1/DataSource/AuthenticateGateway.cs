using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public class AuthenticateGateway : IAuthenticateGateway
    {
        private readonly IAppDbContext appDbContext;

        public AuthenticateGateway(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }

        public IEnumerable<Staff> holder()
        {
            throw new NotImplementedException();
        }

        public Staff RetrievePass(int StaffID)
        {
            //database code to retrieve staff based on id
            return null;

        }
    }
}
