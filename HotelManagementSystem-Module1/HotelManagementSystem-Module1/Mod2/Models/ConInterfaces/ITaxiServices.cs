using HotelManagementSystem.Models.ConEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * ITaxiServices Interface
    */
    public interface ITaxiServices : IConServices
    {
        /*
         * <summary>
         * Request to add the Taxi booking item to database
         * </summary>
         * <returns>
         * bool upon succesfully adding item to the database
         * <returns>
        */
        public Task<bool> AddTaxiBooking(TaxiConBooking taxiConBooking);

        /*
         * <summary>
         * Request to retrieve taxi bookings
         * </summary>
         * <returns>
         * List of TaxiConBooking Object
         * <returns>
        */
        public List<ConBooking> RetrieveTaxi(string field, string filter);

        /*
         * <summary>
         * Request to retrieve taxi bookings by a ID
         * </summary>
         * <returns>
         * TaxiConBooking Object
         * <returns>
        */
        public TaxiConBooking RetrieveTaxiBookingByConID(string ConBookingID);

    }
}
