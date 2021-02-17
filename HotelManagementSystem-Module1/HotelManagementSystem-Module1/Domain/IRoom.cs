using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.Domain
{
    public interface IRoom
    {
        Room ViewRoomSummary(int roomNumber, string roomType);
        IEnumerable<Room> ViewAvailability(int floor, string roomType, bool isSmoking, int roomCapacity);
        bool CreateRoom(int roomNumber, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking);
        bool EditRoom(int roomID, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking);
        bool DeleteRoom(int roomID);
        IEnumerable<Room> RetrieveRoomList();
    }
}
