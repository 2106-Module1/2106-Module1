using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HotelManagementSystem.Domain
{
    public interface IAuthenticate
    {
        bool AuthenticateLogin(string staff_user, string staff_password);
        bool AuthenticatePin(string pin);

        Staff RetrieveStaff(string staff_user);
    }
}
