using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem.Domain
{
    public interface IStaff
    {
        public void UpdateStaffList(IEnumerable<Staff> inStaffList);

        public string StaffPasswordDetail();
    }
}
