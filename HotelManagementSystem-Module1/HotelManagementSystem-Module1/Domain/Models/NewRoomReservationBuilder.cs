using HotelManagementSystem.DataSource;
using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Domain.Models
{
    public class NewRoomReservationBuilder : IReservationBuilder
    {
        private readonly IPromoCodeService _promoCodeService;
        private readonly IGuestService _guestService;
        private readonly IRoomGateway _roomGateway;

        private readonly Reservation _reservation = new Reservation();
        internal bool Flag = true;


        public NewRoomReservationBuilder(IPromoCodeService promoCodeService, IGuestService guestService, IRoomGateway roomGateway)
        {
            _promoCodeService = promoCodeService;
            _guestService = guestService;
            _roomGateway = roomGateway;
        }

        public void GuestReservationBuilder(int guestId)
        {
            var guestBuilderFlag = false;
            if (_guestService.SearchByGuestId(guestId) != null)
            {
                _reservation.SetReservationItem("GuestId", guestId);
                guestBuilderFlag = true;
            }
            Flag = guestBuilderFlag;
        }
        public void ReservationDatesBuilder(DateTime start, DateTime end)
        {
            if (CheckDates(start, end))
            {
                _reservation.SetReservationItem("Start", start);
                _reservation.SetReservationItem("End", end);
            }
            else
            {
                Flag = false;
            }
        }

        public void ReservationRoomBuilder(string roomType, int noOfGuest, string promo)
        {
            var roomBuilderFlag = false;
            _reservation.SetReservationItem("Promo", promo);

            if (ValidateRoomType(roomType))
            {
                _reservation.SetReservationItem("RoomType", roomType);
                roomBuilderFlag = true;
            }

            // Check if there is a Promo Code given
            if (promo != "")
            {
                // Check if the Promo Code given is valid
                if (ValidatePromo(promo))
                {
                    _reservation.SetReservationItem("Price", GetDiscountPrice(roomType, promo));
                    roomBuilderFlag = true;
                }
            }
            else
            {
                _reservation.SetReservationItem("Price", GetRoomPrice(roomType));
                roomBuilderFlag = true;
            }

            // Check if Number of Guest does not exceed the Room Capacity or is below 0 
            if (RoomTypeToGuestNum(roomType, noOfGuest))
            {
                _reservation.SetReservationItem("NoOfGuest", noOfGuest);
                roomBuilderFlag = true;
            }

            Flag = roomBuilderFlag;
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
            return Flag;
        }

        /*
         * Start of Validators Unique to the Builder class
         */
        public Dictionary<string, Dictionary<string, dynamic>> GetRoomDetails()
        {
            var roomDetailList = new Dictionary<string, Dictionary<string, dynamic>>();
            IEnumerable<Room> retrievedList = _roomGateway.GetAllRooms();

            foreach (var data in retrievedList)
            {
                if (!roomDetailList.ContainsKey(data.RoomTypeDetail()))
                {
                    var room = new Dictionary<string, dynamic>
                    {
                        { "Capacity", data.CapacityDetail() },
                        { "Price", data.RoomPriceDetail() }
                    };

                    roomDetailList.Add(data.RoomTypeDetail(), room);
                }
            }

            return roomDetailList;
        }

        private bool ValidatePromo(string promo)
        {
            // Validate if given Promo Code is valid
            PromoCode resPromoCode = _promoCodeService.GetPromoCode(promo);
            if (resPromoCode != null)
            {
                return true;
            }
            return false;
        }

        internal bool ValidateRoomType(string roomType)
        {
            var roomTypeList = GetRoomDetails();

            if (roomTypeList.ContainsKey(roomType))
            {
                return true;
            }
            return false;
        }

        private double GetRoomPrice(string roomType)
        {
            var roomTypeList = GetRoomDetails();
            var roomDetails = roomTypeList[roomType];

            var numOfDays = NumOfDays((DateTime)_reservation.GetReservation()["start"], (DateTime)_reservation.GetReservation()["end"]);

            return roomDetails["Price"] * numOfDays;
        }

        private double GetDiscountPrice(string roomType, string promo)
        {
            var initialPrice = GetRoomPrice(roomType);

            // Retrieve PromoCode Object
            PromoCode resPromoCode = _promoCodeService.GetPromoCode(promo);

            // get the last two digit of the promo Code which will be the discount % and factor into room price
            var discount = (int)resPromoCode.GetPromoCode()["discount"];
            return initialPrice - (initialPrice * (discount / 100.0));
        }

        private bool RoomTypeToGuestNum(string roomType, int numOfGuest)
        {
            var roomTypeList = GetRoomDetails();
            var roomDetails = roomTypeList[roomType];

            if (numOfGuest > roomDetails["Capacity"] || numOfGuest <= 0)
            {
                return false;
            }
            return true;
        }

        private bool CheckDates(DateTime start, DateTime end)
        {
            var now = DateTime.Now;

            // Check if current date is less then reservation date
            // Check if current year <= reservation year, (current month = reservation month, current day must be less than reservation day)
            // if not (current month must be less than reservation day)
            if (now.Year <= start.Year && ((now.Month == start.Month && now.Day < start.Day) || now.Month < start.Month))
            {
                // similarly check if start date is less than end date
                if (start.Year <= end.Year && ((start.Month == end.Month && start.Day < end.Day) || start.Month < end.Month))
                {
                    return true;
                }
                // Error: Start Date is more than End Date
            }
            // Error: Current Date is more than Start Date
            return false;
        }

        private int NumOfDays(DateTime start, DateTime end)
        {
            return (end - start).Days;
        }
        /*
         * End of Validators 
         */
    }
}
