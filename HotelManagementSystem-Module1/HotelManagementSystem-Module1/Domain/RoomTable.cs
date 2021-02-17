using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem_Module1.Models
{
    public class RoomTable: IRoom
    {
        private List<Room> roomList;
        private void SetRoomList(List<Room> inRoomList)
        {
            roomList = inRoomList;
        }
        private List<Room> GetRoomList()
        {
            return roomList;
        }
        public void ModifyRoomList(List<Room> inRoomList)
        {
            SetRoomList(inRoomList);
        }

        public Room ViewRoomSummary(int roomNumber, string roomType)
        {
            throw new NotImplementedException();
        }

        public List<Room> ViewAvailability(int floor, string roomType, string roomStatus, bool isSmoking, int roomCapacity)
        {
            throw new NotImplementedException();
        }

        public bool CreateRoom(int roomNumber, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            throw new NotImplementedException();
        }

        public bool EditRoom(int roomID, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRoom(int roomID)
        {
            throw new NotImplementedException();
        }

        public List<Room> RetrieveRoomList()
        {
            return GetRoomList();
        }

        public RoomTable() { }
        public RoomTable(List<Room> inRoomList)
        {
            SetRoomList(inRoomList);
        }
        
    }
}
