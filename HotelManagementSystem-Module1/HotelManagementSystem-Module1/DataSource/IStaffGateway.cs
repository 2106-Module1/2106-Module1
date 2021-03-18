using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IStaffGateway
    {
      
        Staff RetreieveStaffDetails(string username ,string password);

        void InsertStaff(Staff entity);

        void UpdateStaffDetails(Staff modifiedStaff);
    }
}
