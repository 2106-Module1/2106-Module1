using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * ITaxiReservationDAO Interface
    */
    public interface ITaxiReservationDAO : IConReservationDAO
    {
        /*
          * <summary>
          * Add TaxiConBooking item to the database
          * </summary>
          * <returns>
          * bool upon succesfully adding item to the database
          * <returns>
         */
        public Task<bool> InsertTaxiConRes(TaxiConBooking taxiConBooking);

        /*
         * <summary>
         * Retrieve all taxi bookings
         * </summary>
         * <returns>
         * List of TaxiConBooking Object
         * <returns>
        */
        public List<TaxiConBooking> RetrieveAllTaxi();

        /*
         * <summary>
         * Retrieve taxi bookings by a ID
         * </summary>
         * <returns>
         * TaxiConBooking Object
         * <returns>
        */
        public TaxiConBooking RetrieveTaxiBookingByConID(string ConBookingID);


    }
}
