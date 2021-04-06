using HotelManagementSystem.DataSource;
using System;

namespace HotelManagementSystem.Domain.Models
{
    public class ReservationBuilder : IReservationBuilder
    {
        private readonly IPromoCodeService _promoCodeService;
        private readonly IGuestService _guestService;
        private readonly IRoomGateway _roomGateway;
        private readonly IReservationValidator _reservationValidator;

        private readonly Reservation _reservation = new Reservation();
        private bool _flag = true;
        private int _days = 1;

        public ReservationBuilder(IPromoCodeService promoCodeService, IGuestService guestService, IRoomGateway roomGateway)
        {
            _promoCodeService = promoCodeService;
            _guestService = guestService;
            _roomGateway = roomGateway;
            _reservationValidator = InitValidator();
        }

        public void GuestReservationBuilder(int guestId)
        {
            if (_reservationValidator.ValidateGuest(guestId))
            {
                _reservation.SetReservationItem("GuestId", guestId);
            }
            else
            {
                _flag = false;
            }
        }
        public void ReservationDatesBuilder(DateTime start, DateTime end)
        {
            if (_reservationValidator.CheckDates(start, end) == 0)
            {
                _reservation.SetReservationItem("Start", start);
                _reservation.SetReservationItem("End", end);
                _days = _reservationValidator.NumOfDays(start, end);
            }
            else
            {
                _flag = false;
            }
        }

        public void ReservationRoomBuilder(string roomType, int noOfGuest, string promo)
        {
            if (_reservationValidator.ValidateRoomType(roomType))
            {
                _reservation.SetReservationItem("RoomType", roomType);
            }
            else
            {
                _flag = false;
            }

            // Check if there is a Promo Code given
            if (promo != "")
            {
                // Check if the Promo Code given is valid
                if (_reservationValidator.ValidatePromo(promo))
                {
                    _reservation.SetReservationItem("Promo", promo);
                    _reservation.SetReservationItem("Price", _reservationValidator.GetDiscountPrice(roomType, _days, promo));
                }
                else
                {
                    _flag = false;
                }
            }
            else
            {
                _reservation.SetReservationItem("Promo", promo);
                _reservation.SetReservationItem("Price", _reservationValidator.GetRoomPrice(roomType, _days));
            }

            // Check if Number of Guest does not exceed the Room Capacity or is below 0 
            if (_reservationValidator.RoomTypeToGuestNum(roomType, noOfGuest))
            {
                _reservation.SetReservationItem("NoOfGuest", noOfGuest);
            }
            else
            {
                _flag = false;
            }
        }

        public void AdditionalPreferenceBuilder(string remarks)
        {
            _reservation.SetReservationItem("Remark", remarks);
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

        public bool CanBuild()
        {
            return _flag;
        }

        private ReservationValidator InitValidator()
        {
            return new ReservationValidator(_promoCodeService, _guestService, _roomGateway);
        }
    }
}
