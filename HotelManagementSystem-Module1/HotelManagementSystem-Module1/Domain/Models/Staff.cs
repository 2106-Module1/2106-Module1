using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelManagementSystem_Module1.Domain.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int staffID { get; set; }

        [Required(ErrorMessage = "")]
        private string username { get; set; }

        [Required(ErrorMessage = "")]
        private string password { get; set; }

        [Required(ErrorMessage = "")]
        private int pin { get; set; }

        

        public Staff() { }
        public Staff(int staffID, string staffUsername, string staffPassword, int staffPin)
        {
            staffID = staffID;
            username = staffUsername;
            password = staffPassword;
            pin = staffPin;

        }
        //public Staff(int inRoomNumber, string inRoomType, float inRoomPrice, int inRoomCapacity, string inRoomStatus, bool inIsSmoking)
        //{
        //    roomNumber = inRoomNumber;
        //    roomType = inRoomType;
        //    roomPrice = inRoomPrice;
        //    roomCapacity = inRoomCapacity;
        //    roomStatus = inRoomStatus;
        //    isSmoking = inIsSmoking;
        //}
        
        private Staff RetrieveStaff(int staffID)
        {
            return this;
        }

        public Staff GetStaff(int staffID)
        {
            return RetrieveStaff(staffID);
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

        public int StaffPinDetail()
        {
            return pin;
        }

    }
}
