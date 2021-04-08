using HotelManagementSystem.DataSource;
using System;

/*
 * Owner of ReservationBuilder: Mod 1 Team 4
 */
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
        private string _error = "";

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
                _error = "ERROR: Invalid Guest Id!";
            }
        }
        public void ReservationDatesBuilder(DateTime start, DateTime end)
        {
            var dateChecker = _reservationValidator.CheckDates(start, end);
            if (dateChecker == 0)
            {
                _reservation.SetReservationItem("Start", start);
                _reservation.SetReservationItem("End", end);
                _days = _reservationValidator.NumOfDays(start, end);
            }
            else
            {
                _flag = false;
                if (dateChecker == 1)
                {
                    _error = "ERROR: Start Date is more than End Date!";
                }
                else if (dateChecker == 2)
                {
                    _error = "ERROR: Current Date is more than Start Date!";
                }
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
                _error = "ERROR: Invalid Room Type Input!";
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
                    _error = "ERROR: Invalid Promo Code!";
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
                _error = "ERROR: Room type:" + roomType + " cannot fit " + noOfGuest + " guests!";
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

        public (bool, string) CanBuild()
        {
            return (_flag, _error);
        }

        private ReservationValidator InitValidator()
        {
            return new ReservationValidator(_promoCodeService, _guestService, _roomGateway);
        }
    }
}
