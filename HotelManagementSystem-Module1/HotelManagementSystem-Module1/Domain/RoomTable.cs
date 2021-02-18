using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HotelManagementSystem_Module1.DataSource;

namespace HotelManagementSystem_Module1.Domain
{
    public class RoomTable: IRoom
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
                List<Room> updatedList = roomList.ToList();
                int index = updatedList.IndexOf(currentRoom);
                Room updatedRoom = new Room(roomID, currentRoom.RoomNumberDetail(), roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
                updatedList[index] = updatedRoom;
                roomList = updatedList.AsEnumerable();
                return true;
            }
            return false;
            
        }

        private bool RemoveRoom(int roomID)
        {
            Room removedRoom = roomList.Where(entity => entity.RoomIDDetail() == roomID).Single();
            if (removedRoom.StatusDetail() == "Available")
            {
                List<Room> updatedList = roomList.ToList();
                int index = updatedList.IndexOf(removedRoom);
                updatedList.RemoveAt(index);
                roomList = updatedList.AsEnumerable();
                return true;
            }
            return false;
        }
        private bool NewRoom(int roomNumber, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            Room newRoom = new Room(roomNumber, roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
            roomList.Append(newRoom);
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

        public bool CreateRoom(int roomNumber, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            return NewRoom(roomNumber, roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
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

        public RoomTable() { }
        
    }
}
