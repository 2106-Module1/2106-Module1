using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem.Domain
{
    public interface IAuthenticate
    {
        Staff RetrieveStaff();
        Staff AuthenticateLogin();
        bool AuthenticatePin(string command, string pin);
    }
}
