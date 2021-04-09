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

        /// <summary>
        /// Check and validate whether an user exist with the entered username and password
        /// </summary>
        /// <param name="staff_user">username</param>
        /// <param name="staff_password">password</param>
        /// <returns>bool indicated the login success status</returns>
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
