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
        private readonly IRoomGateway roomGateway;
        private IEnumerable<Room> GetRoomList()
        {
            return roomGateway.GetAllRooms();
        }
        private bool UpdateRoom(int roomID, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking) 
        {
            Room currentRoom = roomGateway.FindRoomSummary(roomID);
            currentRoom.UpdateRoom(roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
            roomGateway.Update(currentRoom);
            return true;
        }

        private bool RemoveRoom(int roomID)
        {
            Room checkRoom = roomGateway.FindRoomSummary(roomID);
            if (checkRoom.StatusDetail() == "Available")
            {
                roomGateway.Delete(checkRoom);
                return true;
            }
            return false;
        }
        private bool NewRoom(int roomNumber, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            Room newRoom = new Room(roomNumber, roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
            roomGateway.Insert(newRoom);
            return true;
        }
        public Room ViewRoomSummary(int roomNumber, string roomType)
        {
            return roomGateway.FindRoomSummary(roomNumber, roomType);
        }

        public IEnumerable<Room> ViewAvailability(int floor, string roomType, bool isSmoking, int roomCapacity)
        {
            return roomGateway.FindAvailability(floor, roomType, isSmoking, roomCapacity);
        }

        public bool CreateRoom(int roomNumber, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            return NewRoom(roomNumber, roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
        }

        public bool EditRoom(int roomID, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
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

        public RoomTable() { }
        public RoomTable(IRoomGateway inRoomGateway)
        {
            roomGateway = inRoomGateway;
        }
        
    }
}
