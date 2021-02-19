using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem_Module1.Domain
{
    public interface IAuthenticate
    {
        Staff RetrieveStaff();
        Staff AuthenticateLogin();
        bool AuthenticatePin();
    }
}
