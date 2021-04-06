using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.Domain
{

    public class RoomBuilder : IRoomBuilder
    {
        private Room _room;
        /// <summary>
        /// Default constructor to build a room
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="roomType"></param>
        public RoomBuilder(int roomNumber, string roomType)
        {
            _room = new Room(roomNumber, roomType, 1000.0, 2, "Available", false);
        }
        /// <summary>
        /// Return a Room object after building
        /// </summary>
        /// <returns>Room</returns>
        public Room Build()
        {
            return _room;
        }
        /// <summary>
        /// Set room capacity to build
        /// </summary>
        /// <param name="roomCapacity"></param>
        public void Capacity(int roomCapacity)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), _room.RoomPriceDetail(), roomCapacity, _room.StatusDetail(), _room.SmokingDetail());
        }
        /// <summary>
        /// Set room price to build
        /// </summary>
        /// <param name="roomPrice"></param>
        public void Price(int roomPrice)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), roomPrice, _room.CapacityDetail(), _room.StatusDetail(), _room.SmokingDetail());
        }
        /// <summary>
        /// Set smoking status of room to build
        /// </summary>
        /// <param name="isSmoking"></param>
        public void SmokingPreference(bool isSmoking)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), _room.RoomPriceDetail(), _room.CapacityDetail(), _room.StatusDetail(), isSmoking);
        }
        /// <summary>
        /// Set room status to build
        /// </summary>
        /// <param name="roomStatus"></param>
        public void Status(string roomStatus)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), _room.RoomPriceDetail(), _room.CapacityDetail(), roomStatus, _room.SmokingDetail());
        }
    }
}
