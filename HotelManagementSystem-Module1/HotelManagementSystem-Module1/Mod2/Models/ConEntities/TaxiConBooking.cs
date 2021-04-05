using System;

namespace HotelManagementSystem.Models.ConEntities
{
    /*
    * Author: Mod 2 Team 2
    * TaxiConBooking Entity Class
    */
    public class TaxiConBooking : ConBooking
    {
        private string TaxiPlateNumber { get; set; }
        private int DriverContactNo { get; set; }
        public TaxiConBooking SetTaxiBooking (string conBookingID,int guestId, string bookingStatus, string bookingType, 
            DateTime activityDateTime,  string taxiPlateNumber, int driverContactNo, string guestName)
        {
            TaxiConBooking newObj = new TaxiConBooking();

            newObj.SetConBooking(conBookingID, guestId, bookingStatus, bookingType, activityDateTime, guestName);
            newObj.TaxiPlateNumber = taxiPlateNumber;
            newObj.DriverContactNo = driverContactNo;

            return newObj;
        }

        public void ModifyTaxiBooking(string conBookingID, int guestId, string bookingStatus, string bookingType,
            DateTime activityDateTime, string taxiPlateNumber, int driverContactNo, string guestName)
        {
            SetConBooking(conBookingID, guestId, bookingStatus, bookingType, activityDateTime, guestName);
            TaxiPlateNumber = taxiPlateNumber;
            DriverContactNo = driverContactNo;
        }

        public class ReadOnly : ConBookingReadOnly
        {
            public string TaxiPlateNumber { get; set; }
            public int DriverContactNo { get; set; }
        }

        public ReadOnly RetrieveTaxiObject()
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
                TaxiPlateNumber = TaxiPlateNumber,
                DriverContactNo = DriverContactNo
            };

            return returnObject;
        }
    }
}
