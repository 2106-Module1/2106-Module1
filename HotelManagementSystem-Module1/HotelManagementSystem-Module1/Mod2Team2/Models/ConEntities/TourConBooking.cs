using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConEntities
{
    /*
    * Author: Mod 2 Team 2
    * TourConBooking Entity Class
    */
    public class TourConBooking : ConBooking
    {
        private float TourPrice { get; set; }
        private string TourName { get; set; }
        public TourConBooking SetTourBooking(string conBookingID, int guestId, string bookingStatus, string bookingType,
             DateTime activityDateTime, float tourPrice, string tourName, string guestName)
        {
            TourConBooking newObj = new TourConBooking();

            newObj.SetConBooking(conBookingID, guestId, bookingStatus, bookingType, activityDateTime, guestName);
            newObj.TourPrice = tourPrice;
            newObj.TourName = tourName;

            return newObj;
        }

        public void ModifyTourBooking(string conBookingID, int guestId, string bookingStatus, string bookingType,
             DateTime activityDateTime, float tourPrice, string tourName, string guestName)
        {
            SetConBooking(conBookingID, guestId, bookingStatus, bookingType, activityDateTime, guestName);
            TourPrice = tourPrice;
            TourName = tourName;
        }

        public class ReadOnly : ConBookingReadOnly
        {
            public float TourPrice { get; set; }
            public string TourName { get; set; }
        }

        public ReadOnly RetrieveTourObject()
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
                TourPrice = TourPrice,
                TourName = TourName
            };

            return returnObject;
        }
    }
}
