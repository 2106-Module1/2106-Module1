using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelManagementSystem.Domain
{
    public interface IRoom
    {
        /// <summary>
        /// Return specific room
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="roomType"></param>
        /// <returns>Room</returns>
        Room ViewRoomSummary(int roomNumber, string roomType);

        /// <summary>
        /// Return a list of available rooms based on params
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="roomType"></param>
        /// <param name="isSmoking"></param>
        /// <param name="roomCapacity"></param>
        /// <returns>IEnumerable Collection of Room entities</returns>
        IEnumerable<Room> ViewAvailability(int floor, string roomType, bool isSmoking, int roomCapacity);

        /// <summary>
        /// Check if the new room can be stored in database
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <returns>bool</returns>
        bool CreateRoom(int roomNumber);

        /// <summary>
        /// Check if update room is valid
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="roomType"></param>
        /// <param name="roomPrice"></param>
        /// <param name="roomCapacity"></param>
        /// <param name="roomStatus"></param>
        /// <param name="isSmoking"></param>
        /// <returns>bool</returns>
        bool EditRoom(int roomID, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking);

        /// <summary>
        /// Check if room is allowed to be removed
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>bool</returns>
        bool DeleteRoom(int roomID);

        /// <summary>
        /// Return the list of rooms held by this class
        /// </summary>
        /// <returns>IEnumerable Collection of Room entities</returns>
        IEnumerable<Room> RetrieveRoomList();

        /// <summary>
        /// Set the list of rooms held by this class to the param
        /// </summary>
        /// <param name="inRoomList"></param>
        public void UpdateRoomList(IEnumerable<Room> inRoomList);
    }
}
