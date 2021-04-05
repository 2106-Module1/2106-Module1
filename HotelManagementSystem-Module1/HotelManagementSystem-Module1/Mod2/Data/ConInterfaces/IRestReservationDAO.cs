using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IRestReservationDAO Interface
    */
    public interface IRestReservationDAO : IConReservationDAO
    {
        /*
          * <summary>
          * Add RestaurantConBooking item to the database
          * </summary>
          * <returns>
          * bool upon succesfully adding item to the database
          * <returns>
         */
        public Task<bool> InsertRestConRes(RestaurantConBooking restConBooking);

        /*
       * <summary>
       * Retrieve all Restaurant bookings
       * </summary>
       * <returns>
       * List of RestaurantConBooking Object
       * <returns>
      */
        public List<RestaurantConBooking> RetrieveAllRestaurant();

        /*
          * <summary>
          * Retrieve Restaurant booking by a ID
          * </summary>
          * <returns>
          * RestaurantConBooking Object
          * <returns>
         */
        public RestaurantConBooking RetrieveRestaurantBookingByConID(string ConBookingID);
    }
}
