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
    * RestAdapter Class
    */
    public class RestAdapter : IConAdapter
    {
        private readonly RestaurantConBooking _RestConBooking = new RestaurantConBooking();
        public RestAdapter() { }

        public RestAdapter(Dictionary<string, object> dict)
        {
            string id = (string)dict["id"];
            string RestaurantName = (string)dict["RestaurantName"];
            int GuestId = (int)dict["GuestId"];
            DateTime ActivityDateTime = (DateTime)dict["ActivityDateTime"];
            string GuestName = (string)dict["GuestName"];

            _RestConBooking = _RestConBooking.SetRestaurantBooking(id, GuestId, "BOOKED", "Restaruant", ActivityDateTime, RestaurantName, GuestName);
        }

        public ConBooking GetConBooking()
        {
            return _RestConBooking;
        }
    }
}
