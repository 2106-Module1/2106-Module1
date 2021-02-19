using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;

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

        public Staff getPassword(int StaffID)
        {
            //database code to retrieve staff based on id
            return null;

        }

        public String RetrievePin()
        {
            //database code to retrieve daily pin
            return null;
        }
    }
}
