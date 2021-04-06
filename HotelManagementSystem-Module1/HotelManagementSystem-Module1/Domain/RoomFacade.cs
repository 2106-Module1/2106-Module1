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
        /// <summary>
        /// Return model containing all rooms
        /// </summary>
        /// <returns>IRoom</returns>
        public IRoom RetrieveAllRoom()
        {
            IEnumerable<Room> retrievedList = roomGateway.GetAllRooms();
            roomTable.UpdateRoomList(retrievedList);
            return roomTable;
        }
        /// <summary>
        /// Return model containing all rooms that are available
        /// </summary>
        /// <returns>IRoom</returns>
        public IRoom RetrieveAvailableRoom()
        {
            IEnumerable<Room> retrievedList = roomGateway.FindAvailability();
            roomTable.UpdateRoomList(retrievedList);
            return roomTable;
        }
        /// <summary>
        /// Return model containing all rooms that are available based on params
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="roomType"></param>
        /// <param name="smokingRoom"></param>
        /// <param name="capacity"></param>
        /// <returns>IRoom</returns>
        public IRoom RetrieveAvailableRoom(int floor, string roomType, bool smokingRoom, int capacity)
        {
            IEnumerable<Room> retrievedList = roomGateway.FindAvailability(floor, roomType, smokingRoom, capacity);
            roomTable.UpdateRoomList(retrievedList);
            return roomTable;
        }
        /// <summary>
        /// Process the update of room based on params
        /// Return a boolean value indicating the success status
        /// </summary>
        /// <param name="RoomIDDetail"></param>
        /// <param name="RoomTypeDetail"></param>
        /// <param name="RoomPriceDetail"></param>
        /// <param name="RoomCapacityDetail"></param>
        /// <param name="RoomStatusDetail"></param>
        /// <param name="RoomSmokingDetail"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Process the removal of room based on room id
        /// Return a boolean value indicating the success status
        /// </summary>
        /// <param name="RoomIDDetail"></param>
        /// <returns>bool</returns>
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
        /// <summary>
        /// Process the creation of new room built based on params
        /// Return a boolean value indicating the success status
        /// </summary>
        /// <param name="RoomNumberDetail"></param>
        /// <param name="RoomTypeDetail"></param>
        /// <param name="RoomPriceDetail"></param>
        /// <param name="RoomCapacityDetail"></param>
        /// <param name="RoomStatusDetail"></param>
        /// <param name="RoomSmokingDetail"></param>
        /// <returns>bool</returns>
        public bool CreateRoom(int RoomNumberDetail, string RoomTypeDetail, int RoomPriceDetail, int RoomCapacityDetail, string RoomStatusDetail, bool RoomSmokingDetail)
        {
            IEnumerable<Room> retrievedList = roomGateway.GetAllRooms();
            roomTable.UpdateRoomList(retrievedList);

            IRoomBuilder RoomBuild = new RoomBuilder(RoomNumberDetail, RoomTypeDetail);
           
            if (RoomPriceDetail > 0)
            {
                RoomBuild.Price(RoomPriceDetail);
            }

            Room newRoom = RoomBuild.Build();

            if (roomTable.CreateRoom(newRoom.RoomNumberDetail()))
            {
                roomGateway.Insert(newRoom);
                return true;
            }
            return false;
        }
    }
}
