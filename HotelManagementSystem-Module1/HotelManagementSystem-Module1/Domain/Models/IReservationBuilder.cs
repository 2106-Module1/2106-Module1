using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public interface IReservationBuilder
    {
        void GuestReservationBuilder(int guestId);
        void ReservationRoomBuilder(string roomType, int noOfGuest, string promo);
        void ReservationDatesBuilder(DateTime start, DateTime end);
        void AdditionalPreferenceBuilder(string remarks);
        void ReservationStateBuilder(string status, DateTime modDateTime);
        bool CanBuild();
        Reservation GetNewReservation();
    }
}
