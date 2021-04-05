using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConEntities
{
    /*
    * Author: Mod 2 Team 2
    * RestaurantConBooking Entity Class
    */
    public class RestaurantConBooking : ConBooking
    {
        private string RestaurantName { get; set; }

        public RestaurantConBooking SetRestaurantBooking(string conBookingID, int guestId, string bookingStatus, string bookingType,
             DateTime activityDateTime, string restaurantName, string guestName)
        {
            RestaurantConBooking newObj = new RestaurantConBooking();

            newObj.SetConBooking(conBookingID, guestId, bookingStatus, bookingType, activityDateTime, guestName);
            newObj.RestaurantName = restaurantName;

            return newObj;
        }

        public void ModifyRestaurantBooking(string conBookingID, int guestId, string bookingStatus, string bookingType,
             DateTime activityDate, string restaurantName, string guestName)
        {
            SetConBooking(conBookingID, guestId, bookingStatus, bookingType, activityDate, guestName);
            RestaurantName = restaurantName;
        }

        public class ReadOnly : ConBookingReadOnly
        {
            public string RestaurantName { get; set; }
        }

        public ReadOnly RetrieveRestObject()
        {
            ConBookingReadOnly ReadOnly = RetrieveConObject();
            ReadOnly returnObject = new ReadOnly
            {
                Id = ReadOnly.Id,
                GuestId = ReadOnly.GuestId,
                TransactionDateTime = ReadOnly.TransactionDateTime,
                BookingStatus = ReadOnly.BookingStatus,
                BookingType = ReadOnly.BookingType,
                ActivityDateTime = ReadOnly.ActivityDateTime,
                GuestName = ReadOnly.GuestName,
                RestaurantName = RestaurantName
            };

            return returnObject;
        }
    }
}
