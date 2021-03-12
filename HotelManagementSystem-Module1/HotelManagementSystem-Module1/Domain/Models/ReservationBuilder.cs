using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class ReservationBuilder : IReservationBuilder
    {
        private Reservation _reservation = new Reservation();

        public void GuestReservationBuilder(int guestId)
        {
            _reservation.SetReservationItem("GuestId", guestId);
        }

        public void ReservationRoomBuilder(string roomType, double price, int noOfGuest)
        {
            _reservation.SetReservationItem("RoomType", roomType);
            _reservation.SetReservationItem("Price", price);
            _reservation.SetReservationItem("NoOfGuest", noOfGuest);
        }

        public void ReservationDatesBuilder(DateTime start, DateTime end)
        {
            _reservation.SetReservationItem("Start", start);
            _reservation.SetReservationItem("End", end);
        }

        public void AdditionalPreferenceBuilder(string remarks, string promo)
        {
            _reservation.SetReservationItem("Remark", remarks);
            _reservation.SetReservationItem("Promo", promo);
        }
        

        public void ReservationStateBuilder(string status, DateTime modDateTime)
        {
            _reservation.SetReservationItem("Status", status);
            _reservation.SetReservationItem("Mod", modDateTime);
        }

        public Reservation GetNewReservation()
        {
            return _reservation;
        }

    }

}
