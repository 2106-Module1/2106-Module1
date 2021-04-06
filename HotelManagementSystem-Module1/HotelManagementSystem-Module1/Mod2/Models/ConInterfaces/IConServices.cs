using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IConServices Interface
    */
    public interface IConServices
    {

        /*
         * <summary>
         * Generates a ID for the Booking based on the booking type
         * </summary>
         * <returns>
         * String combining Concierge "C" for Concierge booking, DateTime and the GuestID
         * <returns>
        */
        public string GenerateID(DateTime dateTime, int guestID);

        /*
         * <summary>
         * Request to updates the booking status of an item to 'CANCELLED'.
         * Item will have its booking status changed to 'CANCELLED' if it is not already Cancelled
         * </summary>
         * <returns>
         * bool upon successfully changing status to 'CANCELLED'.
         * <returns>
        */
        public Task<bool> UpdateBookingStatusCancelled(string conBookingID);

    }
}
