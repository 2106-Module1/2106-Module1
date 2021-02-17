using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementSystem_Module1.Models
{
    public interface IRoom
    {
        void ModifyRoomList(List<Room> roomList);
        Room ViewRoomSummary(int roomNumber, string roomType);
        List<Room> ViewAvailability(int floor, string roomType, string roomStatus, bool isSmoking, int roomCapacity);
        bool CreateRoom(int roomNumber, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking);
        bool EditRoom(int roomID, string roomType, float roomPrice, int roomCapacity, string roomStatus, bool isSmoking);
        bool DeleteRoom(int roomID);
        List<Room> RetrieveRoomList();
    }
}
