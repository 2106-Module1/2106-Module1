using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManagementSystem_Module1.Domain.Models
{
    public class Room
    {
        [Key]
        private int roomID { get; set; }

        [Required(ErrorMessage = "Room number is required ")]
        private int roomNumber { get; set; }

        [Required(ErrorMessage = "Room Type is required")]
        private string roomType { get; set; }

        [Required(ErrorMessage = "Room Price is required")]
        private float roomPrice { get; set; }

        [Required(ErrorMessage = "Room Capacity is required")]
        private int roomCapacity { get; set; }

        [Required(ErrorMessage = "Room Status is required ")]
        private string roomStatus { get; set; }

        [Required(ErrorMessage = "Smoking Status is required ")]
        private bool isSmoking { get; set; }

        public Room() { }
        public Room(int inRoomID, int inRoomNumber, string inRoomType, float inRoomPrice, int inRoomCapacity, string inRoomStatus, bool inIsSmoking)
        {
            roomID = inRoomID;
            roomNumber = inRoomNumber;
            roomType = inRoomType;
            roomPrice = inRoomPrice;
            roomCapacity = inRoomCapacity;
            roomStatus = inRoomStatus;
            isSmoking = inIsSmoking;
        }

        private Room RetrieveRoom()
        {
            return this;
        }

        public Room GetRoom()
        {
            return RetrieveRoom();
        }
    }
}
