using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * TaxiAdapter Class
    */
    public class TaxiAdapter : IConAdapter
    {
        private readonly TaxiConBooking _taxiConBooking = new TaxiConBooking();
        public TaxiAdapter() { }

        public TaxiAdapter(Dictionary<string, object> dict)
        {
            string id = (string)dict["id"];
            string TaxiPlateNumber = (string)dict["TaxiPlateNumber"];
            int DriverContactNo = (int)dict["DriverContactNo"];
            int GuestId = (int)dict["GuestId"];
            DateTime ActivityDateTime = (DateTime)dict["ActivityDateTime"];
            string GuestName = (string)dict["GuestName"];

            _taxiConBooking = _taxiConBooking.SetTaxiBooking(id, GuestId, "BOOKED", "Taxi", ActivityDateTime, TaxiPlateNumber, DriverContactNo, GuestName);
        }
        public ConBooking GetConBooking()
        {
            return _taxiConBooking;
        }
    }
}
