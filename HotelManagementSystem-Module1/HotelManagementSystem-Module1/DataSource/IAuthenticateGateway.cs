using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public interface IAuthenticateGateway
    {
        IEnumerable<Staff> holder();
        public Staff RetrievePass(int StaffID);

    }
}
