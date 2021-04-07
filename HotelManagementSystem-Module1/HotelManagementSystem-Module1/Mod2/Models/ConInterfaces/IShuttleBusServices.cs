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
    * IShuttleBusServices Interface
    */
    public interface IShuttleBusServices
    {

        /*
         * <summary>
         * Generate a unique ID for ShuttleBus object
         * </summary>
         * <returns>
         * String combining the ID number following the last index in the ShuttleBus database
         * THIS ASSUMES THAT THE ID IS PURELY NUMERICAL
         * WE SHOULD PROBABLY SETTLE ON AN ID FORMAT OR MAKE EXCEPTIONS FOR THAT
         * but still low priority. this is only useful if we add/delete buses.
         * <returns>
        */
        //public string GenerateID();

        /// <summary>
        /// Request to adds a new Shuttle Bus into the database
        /// </summary>
        /// <returns>
        /// returns true if adding is successful. returns false if not.
        /// </returns>
        public Task<bool> AddShuttleBus(ShuttleBus shuttleBus);

        /// <summary>
        /// Request to remove a new Shuttle Bus into the database
        /// </summary>
        /// <returns>
        /// returns true if removal  is successful. returns false if not.
        /// </returns>
        public Task<bool> RemoveShuttleBus(ShuttleBus shuttleBus);
    }
}
