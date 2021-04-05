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

        public RoomBuilder(int roomNumber, string roomType)
        {
            _room = new Room(roomNumber, roomType, 1000.0, 2, "Available", false);
        }

        public Room Build()
        {
            return _room;
        }

        public void Capacity(int roomCapacity)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), _room.RoomPriceDetail(), roomCapacity, _room.StatusDetail(), _room.SmokingDetail());
        }

        public void Price(int roomPrice)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), roomPrice, _room.CapacityDetail(), _room.StatusDetail(), _room.SmokingDetail());
        }

        public void SmokingPreference(bool isSmoking)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), _room.RoomPriceDetail(), _room.CapacityDetail(), _room.StatusDetail(), isSmoking);
        }

        public void Status(string roomStatus)
        {
            _room.UpdateRoom(_room.RoomTypeDetail(), _room.RoomPriceDetail(), _room.CapacityDetail(), roomStatus, _room.SmokingDetail());
        }
    }
}
