using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IStaffGateway
    {
        Staff getPassword(int staff_id);

        IEnumerable<Staff> RetreieveStaffDetails();

        void UpdateStaffDetails(Staff modifiedStaff);
    }
}
