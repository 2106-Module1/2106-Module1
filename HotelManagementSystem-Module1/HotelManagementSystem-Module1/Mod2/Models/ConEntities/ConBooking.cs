using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConEntities
{
    /*
    * Author: Mod 2 Team 2
    * ConBooking Abstract Entity Class
    */
    public abstract class ConBooking
    {
        [Key]
        private string Id { get; set; }
        private int GuestId { get; set; }
        private DateTime TransactionDateTime { get; set; }
        private string BookingStatus { get; set; }
        private string BookingType { get; set; }
        private DateTime ActivityDateTime { get; set; }
        private string GuestName { get; set; }
        protected ConBooking() { }
        public ConBooking(string conBookingID, int guestID, string bookingStatus, string bookingType, DateTime activityDateTime, string guestName)
        {
            Id = conBookingID;
            GuestId = guestID;
            TransactionDateTime = DateTime.Now;
            BookingStatus = bookingStatus;
            BookingType = bookingType;
            ActivityDateTime = activityDateTime;
            GuestName = guestName;
        }
        public void SetConBooking(string conBookingID, int guestID, string bookingStatus, string bookingType, DateTime activityDateTime, string guestName)
        {
            Id = conBookingID;
            GuestId = guestID;
            TransactionDateTime = DateTime.Now;
            BookingStatus = bookingStatus;
            BookingType = bookingType;
            ActivityDateTime = activityDateTime;
            GuestName = guestName;
        }
        public void SetBookingStatus(string status)
        {
            BookingStatus = status;
        }
        public int RetrieveGuestID()
        {
            return GuestId;
        }
        public class ConBookingReadOnly
        {
            public  string Id { get; set; }
            public int GuestId { get; set; }
            public DateTime TransactionDateTime { get; set; }
            public string BookingStatus { get; set; }
            public string BookingType { get; set; }
            public DateTime ActivityDateTime { get; set; }
            public string GuestName { get; set; }
        }
        public ConBookingReadOnly RetrieveConObject()
        {
            ConBookingReadOnly returnObject = new ConBookingReadOnly
            {
                Id = Id,
                GuestId = GuestId,
                TransactionDateTime = TransactionDateTime,
                BookingStatus = BookingStatus,
                BookingType = BookingType,
                ActivityDateTime = ActivityDateTime,
                GuestName = GuestName
            };

            return returnObject;
        }
    }


}
