using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HotelManagementSystem.DataSource;

namespace HotelManagementSystem.Domain
{
    public class RoomManagement: IRoom
    {
        private IEnumerable<Room> roomList;
        /// <summary>
        /// Return room list
        /// </summary>
        /// <returns>IEnumerable Collection of Room entities</returns>
        private IEnumerable<Room> GetRoomList()
        {
            return roomList;
        }
        /// <summary>
        /// Set the roomlist held in this class
        /// </summary>
        /// <param name="inRoomList"></param>
        private void SetRoomList(IEnumerable<Room> inRoomList)
        {
            roomList = inRoomList;
        }
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
        private bool UpdateRoom(int roomID, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking) 
        {
            Room currentRoom = roomList.Where(entity => entity.RoomIDDetail() == roomID).SingleOrDefault();
            if(currentRoom != null)
            {
                currentRoom.UpdateRoom(roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
                return true;
            }
            return false;
    
        }
        /// <summary>
        /// Check if room is allowed to be removed
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>bool</returns>
        private bool RemoveRoom(int roomID)
        {
            Room removedRoom = roomList.Where(entity => entity.RoomIDDetail() == roomID).Single();
            if (removedRoom.StatusDetail() == "Available")
            {
               
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check if the new room can be stored in database
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <returns>bool</returns>
        private bool NewRoom(int roomNumber)
        {
            Room currentRoom = roomList.Where(entity => entity.RoomNumberDetail() == roomNumber).SingleOrDefault();
            if (currentRoom != null)
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// Return specific room
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="roomType"></param>
        /// <returns>Room</returns>
        public Room ViewRoomSummary(int roomNumber, string roomType)
        {
            return roomList.Where(entity => entity.RoomNumberDetail() == roomNumber && entity.RoomTypeDetail() == roomType).Single();
        }
        /// <summary>
        /// Return a list of available rooms based on params
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="roomType"></param>
        /// <param name="isSmoking"></param>
        /// <param name="roomCapacity"></param>
        /// <returns>IEnumerable Collection of Room entities</returns>
        public IEnumerable<Room> ViewAvailability(int floor, string roomType, bool isSmoking, int roomCapacity)
        {
            if(floor == 0 && roomCapacity == 0)
            {
                roomList = roomList.Where(entity => entity.RoomTypeDetail() == roomType && entity.SmokingDetail() == isSmoking);
            } else if(floor != 0 && roomCapacity == 0)
            {
                roomList = roomList.Where(entity => entity.RoomNumberDetail().ToString()[0].ToString() == floor.ToString() && entity.RoomTypeDetail() == roomType && entity.SmokingDetail() == isSmoking);
            } else
            {
                roomList = roomList.Where(entity => entity.CapacityDetail() == roomCapacity && entity.RoomTypeDetail() == roomType && entity.SmokingDetail() == isSmoking);
            }
            return roomList;
        }
        /// <summary>
        /// Check if the new room can be stored in database
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <returns>bool</returns>
        public bool CreateRoom(int roomNumber)
        {
            return NewRoom(roomNumber);
        }
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
        public bool EditRoom(int roomID, string roomType, double roomPrice, int roomCapacity, string roomStatus, bool isSmoking)
        {
            return UpdateRoom(roomID, roomType, roomPrice, roomCapacity, roomStatus, isSmoking);
        }
        /// <summary>
        /// Check if room is allowed to be removed
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>bool</returns>
        public bool DeleteRoom(int roomID)
        {
            return RemoveRoom(roomID);
        }
        /// <summary>
        /// Return the list of rooms held by this class
        /// </summary>
        /// <returns>IEnumerable Collection of Room entities</returns>
        public IEnumerable<Room> RetrieveRoomList()
        {
            return GetRoomList();
        }
        /// <summary>
        /// Set the list of rooms held by this class to the param
        /// </summary>
        /// <param name="inRoomList"></param>
        public void UpdateRoomList(IEnumerable<Room> inRoomList)
        {
            SetRoomList(inRoomList);
        }

        public RoomManagement() { }
        
    }
}
