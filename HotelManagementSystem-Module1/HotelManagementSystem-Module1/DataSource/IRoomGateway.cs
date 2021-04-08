using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public interface IRoomGateway
    {
        /// <summary>
        /// Return all room in database
        /// </summary>
        /// <returns>IEnumerable Collection of Room entities</returns>
        IEnumerable<Room> GetAllRooms();

        /// <summary>
        /// Find specific room based on the room number and type of room
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="roomType"></param>
        /// <returns>Room entity</returns>
        Room FindRoomSummary(int roomNumber, string roomType);

        /// <summary>
        /// Return all room that are available based on param
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="roomType"></param>
        /// <param name="isSmoking"></param>
        /// <param name="roomCapacity"></param>
        /// <returns>IEnumerable Collection of Room entities</returns>
        IEnumerable<Room> FindAvailability(int floor, string roomType, bool isSmoking, int roomCapacity);

        /// <summary>
        /// Return all room in database that are available
        /// </summary>
        /// <returns>IEnumerable Collection of Room entities</returns>
        IEnumerable<Room> FindAvailability();

        /// <summary>
        /// Create a new room record in database
        /// </summary>
        /// <param name="newRoom"></param>
        void Insert(Room newRoom);

        /// <summary>
        /// Save changes
        /// </summary>
        void Update();

        /// <summary>
        /// Remove room from database
        /// </summary>
        /// <param name="room">room to be deleted</param>
        void Delete(Room room);

        /// <summary>
        /// Find specific room based on id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns>Room entity</returns>
        Room FindRoomSummary(int roomId);
    }
}
