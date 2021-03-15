using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem.Domain
{
    public interface IAuthenticate
    {
        Staff RetrieveStaff();
        Staff AuthenticateLogin(string staff_user, string staff_password);
        bool AuthenticatePin(string pin);
    }
}
