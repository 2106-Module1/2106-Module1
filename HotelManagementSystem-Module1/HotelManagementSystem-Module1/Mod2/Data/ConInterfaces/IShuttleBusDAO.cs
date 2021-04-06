using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IShuttleBusDAO Interface
    */
    public interface IShuttleBusDAO
    {
        /*
         * <summary>
         * Adds a shuttle bus into the fleet.
         * Keep in view, but do not implement yet as it is out of scope.
         * </summary>
        */
        public Task<bool> InsertShuttleBus(ShuttleBus shuttleBus);

        /*
         * <summary>
         * Removes a shuttle bus from the fleet.
         * Keep in view, but do not implement yet as it is out of scope.
         * </summary>
        */
        public Task<bool> RemoveShuttleBusById(string busId);

        /*
        * <summary>
        * Returns the total amount of passengers that every bus in the fleet can carry.
        * </summary>
        */
        public int RetrieveTotalFleetCapacity();

        /*
        * <summary>
        * Returns all buses as a List of ShuttleBus objects
        * </summary>
        */
        public List<ShuttleBus> RetrieveAllShuttleBuses();

        /*
        * <summary>
        * Retrieve the ShuttleBus that matches the given Id
        * Returns an empty ShuttleBus if no matches are found.
        * </summary>
        */
        public ShuttleBus RetrieveShuttleBusById(string busId);

    }
}
