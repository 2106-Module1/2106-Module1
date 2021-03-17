using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IRoomGateway
    {
        IEnumerable<Room> GetAllRooms();
        Room FindRoomSummary(int roomNumber, string roomType);
        IEnumerable<Room> FindAvailability(int floor, string roomType, bool isSmoking, int roomCapacity);
        IEnumerable<Room> FindAvailability();
        void Insert(Room newRoom);
        void Update(Room modifiedRoom);
        void Delete(Room room);
        Room FindRoomSummary(int roomId);
    }
}
