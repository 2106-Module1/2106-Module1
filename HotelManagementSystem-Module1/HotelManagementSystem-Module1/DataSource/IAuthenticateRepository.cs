using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IAuthenticateRepository
    {

        string CheckPass(string username);
        string FindPin(string username);
        void UpdatePin(string pin);
        public bool validateLogin(string staff_user, string staff_password);

    }
}
