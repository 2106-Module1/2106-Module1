﻿using HotelManagementSystem.Domain.Models;
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

       public string CheckPass(string username)
        {
            throw new NotImplementedException();
        }

       //public string FindPin(string username)
       // {
       //     throw new NotImplementedException();
       // }

       // public void UpdatePin(string pin)
       // {
       //     throw new NotImplementedException();
       // }

        public bool validateLogin(string staff_user, string staff_password)
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
