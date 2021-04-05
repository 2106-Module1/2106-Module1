using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Data.ConInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Data.Mod2Repository;

namespace HotelManagementSystem.Data.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * RestReservationGateway Class
    */
    public class RestReservationGateway : IRestReservationDAO
    {
        private readonly Mod2Context _context;

        public RestReservationGateway(Mod2Context context)
        {
            _context = context;
        }
        public async Task<bool> InsertRestConRes(RestaurantConBooking restConBooking)
        {
            _context.Add(restConBooking);
            //Check for successful changes to database
            //Successful
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            //Fail
            else
            {
                return false;
            }
        }

        public List<RestaurantConBooking> RetrieveAllRestaurant()
        {
            return _context.RestaurantConBooking.ToList();
        }

        public RestaurantConBooking RetrieveRestaurantBookingByConID(string ConBookingID)
        {
            return _context.RestaurantConBooking.Find(ConBookingID);
        }

        public async Task<bool> UpdateConResStatus()
        {
            //Check for successful changes to database
            //Successful
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            //Fail
            else
            {
                return false;
            }
        }
    }
}
