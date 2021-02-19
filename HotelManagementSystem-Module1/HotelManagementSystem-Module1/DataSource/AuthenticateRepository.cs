using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;

namespace HotelManagementSystem_Module1.DataSource
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IAppDbContext appDbContext;

        public AuthenticateRepository(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }

       public string CheckPass(string username)
        {
            throw new NotImplementedException();
        }

       public string FindPin(string username)
        {
            throw new NotImplementedException();
        }

        public void UpdatePin(string pin)
        {
            throw new NotImplementedException();
        }
    }
}
