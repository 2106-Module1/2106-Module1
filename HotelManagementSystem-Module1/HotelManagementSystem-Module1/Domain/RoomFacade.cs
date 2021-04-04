using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.Domain
{
    public class RoomFacade : IRoomFacade
    {
        private readonly IRoom roomTable;
        private readonly IRoomGateway roomGateway;
        public RoomFacade(IRoom inRoomTable, IRoomGateway inRoomGateway)
        {
            roomTable = inRoomTable;
            roomGateway = inRoomGateway;
        }
        /// <summary>
        /// Return model containing room summary based on room id
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>IRoom</returns>
        public IRoom FindRoomSummary(int roomID)
        {
            Room retrievedRoom = roomGateway.FindRoomSummary(roomID);
            IEnumerable<Room> roomList = new Room[] { retrievedRoom };
            roomTable.UpdateRoomList(roomList);
            return roomTable;
        }

        public IRoom RetrieveAllRoom()
        {
            IEnumerable<Room> retrievedList = roomGateway.GetAllRooms();
            roomTable.UpdateRoomList(retrievedList);
            return roomTable;
        }

        public IRoom RetrieveAvailableRoom()
        {
            IEnumerable<Room> retrievedList = roomGateway.FindAvailability();
            roomTable.UpdateRoomList(retrievedList);
            return roomTable;
        }

        public IRoom RetrieveAvailableRoom(int floor, string roomType, bool smokingRoom, int capacity)
        {
            IEnumerable<Room> retrievedList = roomGateway.FindAvailability(floor, roomType, smokingRoom, capacity);
            roomTable.UpdateRoomList(retrievedList);
            return roomTable;
        }

        public bool UpdateRoom(int RoomIDDetail, string RoomTypeDetail, int RoomPriceDetail, int RoomCapacityDetail, string RoomStatusDetail, bool RoomSmokingDetail)
        {
            IEnumerable<Room> retrievedList = roomGateway.GetAllRooms();
            roomTable.UpdateRoomList(retrievedList);
            if (roomTable.EditRoom(RoomIDDetail, RoomTypeDetail, RoomPriceDetail, RoomCapacityDetail, RoomStatusDetail, RoomSmokingDetail))
            {
                roomGateway.Update();
                return true;
            }
            return false;
        }
        public bool DeleteRoom(int RoomIDDetail)
        {
            IEnumerable<Room> retrievedList = roomGateway.GetAllRooms();
            roomTable.UpdateRoomList(retrievedList);
            if (roomTable.DeleteRoom(RoomIDDetail))
            {
                roomGateway.Delete(roomGateway.FindRoomSummary(RoomIDDetail));
                return true;
            }
            return false;
        }
    }
}
