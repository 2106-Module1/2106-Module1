using HotelManagementSystem.Models.ConEntities;
using System.Collections.Generic;

namespace HotelManagementSystem.Models.RetrieveInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IRetrieve Interface
    */
    public interface IRetrieve
    {
        /*
         * <summary>
         * Based on input returns a list of booking logs
         * </summary>
         * <returns>
         * list of filtered TaxiConBookings
         * <returns>
        */
        public List<ConBooking> Retrieve(string filter);
    }
}
