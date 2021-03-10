using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HotelManagementSystem.Domain.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int roomID { get; set; }

        [Required(ErrorMessage = "Room number is required ")]
        private int roomNumber { get; set; }

        [Required(ErrorMessage = "Room Type is required")]
        private string roomType { get; set; }

        [Required(ErrorMessage = "Room Price is required")]
        private double roomPrice { get; set; }

        [Required(ErrorMessage = "Room Capacity is required")]
        private int roomCapacity { get; set; }

        [Required(ErrorMessage = "Room Status is required ")]
        private string roomStatus { get; set; }

        [Required(ErrorMessage = "Smoking Status is required ")]
        private bool isSmoking { get; set; }

        public Room() { }
        public Room(int inRoomID, int inRoomNumber, string inRoomType, double inRoomPrice, int inRoomCapacity, string inRoomStatus, bool inIsSmoking)
        {
            roomID = inRoomID;
            roomNumber = inRoomNumber;
            roomType = inRoomType;
            roomPrice = inRoomPrice;
            roomCapacity = inRoomCapacity;
            roomStatus = inRoomStatus;
            isSmoking = inIsSmoking;
        }
        public Room(int inRoomNumber, string inRoomType, double inRoomPrice, int inRoomCapacity, string inRoomStatus, bool inIsSmoking)
        {
            roomNumber = inRoomNumber;
            roomType = inRoomType;
            roomPrice = inRoomPrice;
            roomCapacity = inRoomCapacity;
            roomStatus = inRoomStatus;
            isSmoking = inIsSmoking;
        }
        private void SetRoom(string inRoomType, double inRoomPrice, int inRoomCapacity, string inRoomStatus, bool inIsSmoking)
        {
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
        public void UpdateRoom(string inRoomType, double inRoomPrice, int inRoomCapacity, string inRoomStatus, bool inIsSmoking)
        {
            SetRoom(inRoomType, inRoomPrice, inRoomCapacity, inRoomStatus, inIsSmoking);
        }

        public int RoomNumberDetail()
        {
            return roomNumber;
        }

        public string RoomTypeDetail()
        {
            return roomType;
        }

        public bool SmokingDetail()
        {
            return isSmoking;
        }

        public int CapacityDetail()
        {
            return roomCapacity;
        }

        public string StatusDetail()
        {
            return roomStatus;
        }

        public int RoomIDDetail()
        {
            return roomID;
        }

        public double RoomPriceDetail()
        {
            return roomPrice;
        }
    }
}
