using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Domain
{
    public class ReservationValidator : IReservationValidator
    {
        private readonly IPromoCodeService _promoCodeService;
        private readonly IGuestService _guestService;
        private readonly IRoomGateway _roomGateway;

        public ReservationValidator(IPromoCodeService promoCodeService, IGuestService guestService, IRoomGateway roomGateway)
        {
            _promoCodeService = promoCodeService;
            _guestService = guestService;
            _roomGateway = roomGateway;
        }

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

        public bool ValidateGuest(int guestId)
        {
            if (_guestService.SearchByGuestId(guestId) != null)
            {
                return true;
            }

            return false;
        }

        public bool ValidatePromo(string promo)
        {
            // Validate if given Promo Code is valid
            PromoCode resPromoCode = _promoCodeService.GetPromoCode(promo);
            if (resPromoCode != null)
            {
                return true;
            }
            return false;
        }

        public bool ValidateRoomType(string roomType)
        {
            var roomTypeList = GetRoomDetails();

            if (roomTypeList.ContainsKey(roomType))
            {
                return true;
            }
            return false;
        }

        public double GetRoomPrice(string roomType, int days)
        {
            var roomTypeList = GetRoomDetails();
            var roomDetails = roomTypeList[roomType];

            return roomDetails["Price"] * days;
        }

        public double GetDiscountPrice(string roomType, int days, string promo)
        {
            var initialPrice = GetRoomPrice(roomType, days);

            // Retrieve PromoCode Object
            PromoCode resPromoCode = _promoCodeService.GetPromoCode(promo);

            // get the last two digit of the promo Code which will be the discount % and factor into room price
            var discount = (int)resPromoCode.GetPromoCode()["discount"];
            return initialPrice - (initialPrice * (discount / 100.0));
        }

        public bool RoomTypeToGuestNum(string roomType, int numOfGuest)
        {
            var roomTypeList = GetRoomDetails();
            var roomDetails = roomTypeList[roomType];

            if (numOfGuest > roomDetails["Capacity"] || numOfGuest <= 0)
            {
                return false;
            }
            return true;
        }

        public int CheckDates(DateTime start, DateTime end)
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
                    return 0;
                }
                else
                {
                    return 1;
                }
                // Error: Start Date is more than End Date
            }
            // Error: Current Date is more than Start Date
            return 2;
        }

        public int NumOfDays(DateTime start, DateTime end)
        {
            return (end - start).Days;
        }
    }
}
