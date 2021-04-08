using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * ITourServices Interface
    */
    public interface ITourServices : IConServices
    {
        /*
         * <summary>
         * Request to add the Tour booking item to database
         * </summary>
         * <returns>
         * bool upon succesfully adding item to the database
         * <returns>
        */
        public Task<bool> AddTourBooking(TourConBooking tourConBooking);

        /*
        * <summary>
        * Request to retrieve tour bookings
        * </summary>
        * <returns>
        * List of TourConBooking Object
        * <returns>
        */
        public List<ConBooking> RetrieveTour(string field, string filter);

        /*
         * <summary>
         * Request to retrieve Tour booking by a ID
         * </summary>
         * <returns>
         * TourConBooking Object
         * <returns>
        */
        public TourConBooking RetrieveTourBookingByConID(string ConBookingID);

    }
}
