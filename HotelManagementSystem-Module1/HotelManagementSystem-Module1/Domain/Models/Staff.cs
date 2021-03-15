using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelManagementSystem.Domain.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private IEnumerable<Staff> staffList;
        private int staffID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        private string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        private string password { get; set; }





        public Staff() { }
        public Staff(int id, string staffUsername, string staffPassword)
        {
            this.staffID = id;
            this.username = staffUsername;
            this.password = staffPassword;

        }


        private Staff RetrieveStaff()
        {
            return this;
        }

        public Staff GetStaff()
        {
            return RetrieveStaff();
        }

        public int StaffIDDetail()
        {
            return staffID;
        }

        public string StaffUsernameDetail()
        {
            return username;
        }

        public string StaffPasswordDetail()
        {
            return password;
        }

        private void SetStaffList(IEnumerable<Staff> inRoomList)
        {
            staffList = inRoomList;
        }

        public void UpdateStaffList(IEnumerable<Staff> inStaffList)
        {
            SetStaffList(inStaffList);
        }

    }
}
