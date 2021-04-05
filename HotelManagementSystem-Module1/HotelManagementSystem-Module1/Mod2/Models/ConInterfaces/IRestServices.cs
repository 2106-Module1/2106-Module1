using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IRestServices Interface
    */
    public interface IRestServices : IConServices
    {
        /*
        * <summary>
        * Request to add the Restaurant booking item to database
        * </summary>
        * <returns>
        * bool upon succesfully adding item to the database
        * <returns>
        */
        public Task<bool> AddRestBooking(RestaurantConBooking restaurantConBooking);

        /*
        * <summary>
        * Request to retrieve all Restaurant bookings
        * </summary>
        * <returns>
        * List of RestaurantConBooking Object
        * <returns>
        */
        public List<ConBooking> RetrieveRestaurant(string field, string filter);

        /*
          * <summary>
          * Request to retrieve Restaurant booking by a ID
          * </summary>
          * <returns>
          * RestaurantConBooking Object
          * <returns>
         */
        public RestaurantConBooking RetrieveRestaurantBookingByConID(string ConBookingID);
    }
}
