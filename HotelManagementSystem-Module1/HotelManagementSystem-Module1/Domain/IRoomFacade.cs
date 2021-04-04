using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain
{
    public interface IRoomFacade
    {
        IRoom RetrieveAvailableRoom();
        IRoom RetrieveAvailableRoom(int floor, string roomType, bool smokingRoom, int capacity);
        IRoom RetrieveAllRoom();
        IRoom FindRoomSummary(int roomID);
        bool UpdateRoom(int RoomIDDetail, string RoomTypeDetail, int RoomPriceDetail, int RoomCapacityDetail, string RoomStatusDetail, bool RoomSmokingDetail);
    }
}
