using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem.Domain
{
    public interface IRoom
    {
        Room ViewRoomSummary(int roomNumber, string roomType);
        IEnumerable<Room> ViewAvailability(int floor, string roomType, bool isSmoking, int roomCapacity);
        bool CreateRoom(int roomNumber, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking);
        bool EditRoom(int roomID, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking);
        bool DeleteRoom(int roomID);
        IEnumerable<Room> RetrieveRoomList();
        public void UpdateRoomList(IEnumerable<Room> inRoomList);
    }
}
