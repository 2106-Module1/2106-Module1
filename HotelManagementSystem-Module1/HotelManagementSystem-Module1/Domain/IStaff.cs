using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem.Domain
{
    public interface IStaff
    {
        IEnumerable<Staff> getStaffsByRole (string role);
        void UpdateStaffList(IEnumerable<Staff> inStaffList);
        IEnumerable<Staff> RetrieveStaffList();
        string StaffPasswordDetail();
        string StaffUsernameDetail();
    }
}
