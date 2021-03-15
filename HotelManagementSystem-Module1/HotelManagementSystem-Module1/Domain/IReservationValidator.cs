using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain
{
    public interface IReservationValidator
    {
        bool ValidateGuest(int guestId);

        bool ValidatePromo(string promo);

        bool ValidateRoomType(string roomType);

        double GetRoomPrice(string roomType, int days);

        double GetDiscountPrice(string roomType, int days, string promo);

        bool RoomTypeToGuestNum(string roomType, int numOfGuest);

        bool CheckDates(DateTime start, DateTime end);

        int NumOfDays(DateTime start, DateTime end);
    }
}
