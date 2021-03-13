/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class QueenRmReservationBuilder : IReservationBuilder
    {
        private readonly IPromoCodeService _promoCodeService;
        private Reservation _reservation = new Reservation();
        internal bool Flag = true;

        public QueenRmReservationBuilder(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        public void GuestReservationBuilder(int guestId)
        {

            _reservation.SetReservationItem("GuestId", guestId);
        }

        public void ReservationRoomBuilder(string roomType, int noOfGuest, string promo)
        {
            _reservation.SetReservationItem("Promo", promo);
            _reservation.SetReservationItem("RoomType", roomType);

            // Check if there is a Promo Code given
            if (promo == "")
            {
                // Check if the Promo Code given is valid
                if (ValidatePromo(promo))
                {
                    _reservation.SetReservationItem("Price", GetDiscountPrice(roomType, promo));
                }
                else
                {
                    Flag = false;
                }
            }
            else
            {
                _reservation.SetReservationItem("Price", GetRoomPrice(roomType));
            }

            // Check if Number of Guest does not exceed the Room Capacity or is below 0 
            if (RoomTypeToGuestNum(roomType, noOfGuest))
            {
                _reservation.SetReservationItem("NoOfGuest", noOfGuest);
            }
            else
            {
                Flag = false;
            }

        }

        public void ReservationDatesBuilder(DateTime start, DateTime end)
        {
            if (CheckDates(start, end) != 0)
            {
                _reservation.SetReservationItem("Start", start);
                _reservation.SetReservationItem("End", end);
            }
            else
            {
                Flag = false;
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
            return Flag;
        }

        /*
        public SuiteRmReservationBuilder Build()
        {
            return CanBuild() ? this : throw new Exception("Cannot Build Object");
        }
        #1#

        /*
         * Start of Validators 
         #1#

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

        public double GetRoomPrice(string roomType)
        {
            // TO REPLACE AFTER UNDERSTANDING ZAC's CODE
            var roomDetailDict = new Dictionary<string, double>()
            {
                { "Twin", 100.0 },
                { "Double", 150.0 },
                { "Family", 300.0 },
                { "Suite", 600.0 }
            };

            return roomDetailDict[roomType];
        }

        public double GetDiscountPrice(string roomType, string promo)
        {
            var initialPrice = GetRoomPrice(roomType);

            // Retrieve PromoCode Object
            PromoCode resPromoCode = _promoCodeService.GetPromoCode(promo);

            // get the last two digit of the promo Code which will be the discount % and factor into room price
            var discount = (int)resPromoCode.GetPromoCode()["discount"];
            return initialPrice - (initialPrice * (discount / 100.0));
        }

        public bool RoomTypeToGuestNum(string roomType, int numOfGuest)
        {
            var roomCap = new Dictionary<string, int>
            {
                {"Twin", 2},
                {"Double", 2},
                {"Family", 4},
                {"Suite", 5}
            };

            if (numOfGuest > roomCap[roomType] || numOfGuest <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
                // Error: Start Date is more than End Date
                return 1;
            }
            // Error: Current Date is more than Start Date
            return 2;
        }
        /*
         * End of Validators 
         #1#

    }
}
*/
