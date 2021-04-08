using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain
{
    public interface IRoomFacade
    {
        /// <summary>
        /// Return model containing all rooms that are available
        /// </summary>
        /// <returns>IRoom</returns>
        IRoom RetrieveAvailableRoom();

        /// <summary>
        /// Return model containing all rooms that are available based on params
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="roomType"></param>
        /// <param name="smokingRoom"></param>
        /// <param name="capacity"></param>
        /// <returns>IRoom</returns>
        IRoom RetrieveAvailableRoom(int floor, string roomType, bool smokingRoom, int capacity);

        /// <summary>
        /// Return model containing all rooms
        /// </summary>
        /// <returns>IRoom</returns>
        IRoom RetrieveAllRoom();

        /// <summary>
        /// Return model containing room summary based on room id
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns>IRoom</returns>
        IRoom FindRoomSummary(int roomID);

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
        bool UpdateRoom(int RoomIDDetail, string RoomTypeDetail, int RoomPriceDetail, int RoomCapacityDetail, string RoomStatusDetail, bool RoomSmokingDetail);

        /// <summary>
        /// Process the removal of room based on room id
        /// Return a boolean value indicating the success status
        /// </summary>
        /// <param name="RoomIDDetail"></param>
        /// <returns>bool</returns>
        bool DeleteRoom(int RoomIDDetail);

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
        bool CreateRoom(int RoomNumberDetail, string RoomTypeDetail, int RoomPriceDetail, int RoomCapacityDetail, string RoomStatusDetail, bool RoomSmokingDetail);
    }
}
