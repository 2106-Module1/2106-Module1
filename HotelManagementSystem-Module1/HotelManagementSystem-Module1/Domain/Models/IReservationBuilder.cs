using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public interface IReservationBuilder
    {
        void GuestReservationBuilder(int guestId);
        void ReservationRoomBuilder(string roomType, double price, int noOfGuest);
        void ReservationDatesBuilder(DateTime start, DateTime end);
        void AdditionalPreferenceBuilder(string remarks, string promo);
        void ReservationStateBuilder(string status, DateTime modDateTime);
        Reservation GetNewReservation();
    }
}
