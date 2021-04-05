using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HotelManagementSystem.DataSource;

namespace HotelManagementSystem.Domain
{
    public class RoomManagement: IRoom
    {
        private IEnumerable<Room> roomList;
        private IEnumerable<Room> GetRoomList()
        {
            return roomList;
        }
        private void SetRoomList(IEnumerable<Room> inRoomList)
        {
            roomList = inRoomList;
        }
        private bool UpdateRoom(int roomID, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking) 
        {
            Room currentRoom = roomList.Where(entity => entity.RoomIDDetail() == roomID).SingleOrDefault();
            if(currentRoom != null)
            {
                currentRoom.UpdateRoom(roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
                return true;
            }
            return false;
    
        }

        private bool RemoveRoom(int roomID)
        {
            Room removedRoom = roomList.Where(entity => entity.RoomIDDetail() == roomID).Single();
            if (removedRoom.StatusDetail() == "Available")
            {
               
                return true;
            }
            return false;
        }

        private bool NewRoom(int roomNumber)
        {
            Room currentRoom = roomList.Where(entity => entity.RoomNumberDetail() == roomNumber).SingleOrDefault();
            if (currentRoom != null)
            {
                return false;
            }
            return true;

        }
        public Room ViewRoomSummary(int roomNumber, string roomType)
        {
            return roomList.Where(entity => entity.RoomNumberDetail() == roomNumber && entity.RoomTypeDetail() == roomType).Single();
        }

        public IEnumerable<Room> ViewAvailability(int floor, string roomType, bool isSmoking, int roomCapacity)
        {
            return roomList.Where(entity => (entity.RoomNumberDetail().ToString()[0].ToString() == floor.ToString()) && entity.RoomTypeDetail() == roomType && entity.SmokingDetail() == isSmoking && entity.CapacityDetail() == roomCapacity && entity.StatusDetail() == "Available");
        }

        public bool CreateRoom(int roomNumber)
        {
            return NewRoom(roomNumber);
        }

        public bool EditRoom(int roomID, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            return UpdateRoom(roomID, roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
        }

        public bool DeleteRoom(int roomID)
        {
            return RemoveRoom(roomID);
        }

        public IEnumerable<Room> RetrieveRoomList()
        {
            return GetRoomList();
        }
        public void UpdateRoomList(IEnumerable<Room> inRoomList)
        {
            SetRoomList(inRoomList);
        }

        public RoomManagement() { }
        
    }
}
