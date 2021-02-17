using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.DataSource;

namespace HotelManagementSystem_Module1.Domain
{
    public class RoomTable: IRoom
    {
        private readonly IRoomGateway roomGateway;
        private IEnumerable<Room> GetRoomList()
        {
            return roomGateway.GetAllRooms();
        }

        public Room ViewRoomSummary(int roomNumber, string roomType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> ViewAvailability(int floor, string roomType, string roomStatus, bool isSmoking, int roomCapacity)
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
            Room checkRoom = roomGateway.FindRoomSummary(roomID);
            if (checkRoom.StatusDetail() == "Available")
            {
                roomGateway.Delete(checkRoom);
                return true;
            }
            return false;
        }

        public IEnumerable<Room> RetrieveRoomList()
        {
            return GetRoomList();
        }

        public RoomTable() { }
        public RoomTable(IRoomGateway inRoomGateway)
        {
            roomGateway = inRoomGateway;
        }
        
    }
}
