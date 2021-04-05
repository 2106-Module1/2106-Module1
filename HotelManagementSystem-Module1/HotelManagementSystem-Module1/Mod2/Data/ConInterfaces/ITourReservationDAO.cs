using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * ITourReservationDAO Interface
    */
    public interface ITourReservationDAO : IConReservationDAO
    {
        /*
        * <summary>
        * Add TourConBooking item to the database
        * </summary>
        * <returns>
        * bool upon succesfully adding item to the database
        * <returns>
        */
        public Task<bool> InsertTourConRes(TourConBooking tourConBooking);

        /*
        * <summary>
        * Retrieve all tour bookings
        * </summary>
        * <returns>
        * List of TourConBooking Object
        * <returns>
       */
        public List<TourConBooking> RetrieveAllTour();

        /*
         * <summary>
         * Retrieve Tour booking by a ID
         * </summary>
         * <returns>
         * TourConBooking Object
         * <returns>
        */
        public TourConBooking RetrieveTourBookingByConID(string ConBookingID);
    }
}
