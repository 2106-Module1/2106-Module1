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
    * TourAdapter Class
    */
    public class TourAdapter : IConAdapter
    {
        private readonly TourConBooking _tourConBooking = new TourConBooking();
        public TourAdapter() { }

        public TourAdapter(Dictionary<string, object> dict)
        {
            string id = (string)dict["id"];
            float TourPrice = (float)dict["TourPrice"];
            string TourName = (string)dict["TourName"];
            int GuestId = (int)dict["GuestId"];
            DateTime ActivityDateTime = (DateTime)dict["ActivityDateTime"];
            string GuestName = (string)dict["GuestName"];

            _tourConBooking = _tourConBooking.SetTourBooking(id, GuestId, "BOOKED", "Tour", ActivityDateTime, TourPrice, TourName, GuestName);
        }
        public ConBooking GetConBooking()
        {
            return _tourConBooking;
        }
    }
}
