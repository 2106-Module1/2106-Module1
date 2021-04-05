using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.DataSource
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IAppDbContext _appDbContext;

        public AuthenticateRepository(IAppDbContext appContext)
        {
            _appDbContext = appContext;
        }


        public bool ValidateLogin(string staff_user, string staff_password)
        {
            if (_appDbContext.StaffDb().AsEnumerable().SingleOrDefault(entity => entity.StaffUsernameDetail() == staff_user && entity.StaffPasswordDetail() == staff_password) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
