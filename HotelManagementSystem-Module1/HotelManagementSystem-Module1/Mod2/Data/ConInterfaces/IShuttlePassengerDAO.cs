using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IShuttlePassengerDAO Interface
    */
    public interface IShuttlePassengerDAO
    {
        /*
         * <summary>
         * Adds a ShuttlePassenger object into the database
         * requires a ShuttlePassenger to be passed in
         * </summary>
        */
        public Task<bool> InsertShuttlePassenger(ShuttlePassenger shuttlePassenger);

        /*
         * <summary>
         * Removes a shuttle passenger
         * </summary>
        */
        public Task<bool> RemoveShuttlePassengerById(string id);

        /*
         * <summary>
         * Returns all passengers (in a list) under a ShuttleSchedule and a Bus
         * Will return an empty list if there are no passengers matching the given busId.
         * </summary>
        */
        public List<ShuttlePassenger> RetrievePassengersOfScheduleIdAndBusId(string scheduleId, string busId);

        /*
         * <summary>
         * Returns all passengers (in a list) of a shuttle schedule
         * Will return an empty list if there are no passengers matching the given shuttle scheduleId.
         * </summary>
        */
        public List<ShuttlePassenger> RetrievePassengersOfScheduleId(string scheduleId);

        /*
         * <summary>
         * Returns all passengers (in a list) of a shuttle bus
         * Will return an empty list if there are no passengers matching the given shuttle busId.
         * </summary>
        */
        public List<ShuttlePassenger> RetrievePassengersOfBusId(string busId);

        /*
         * <summary>
         * Returns the busId of all buses that are booked under a specific schedule. 
         * Returns a list of Strings. List will contain at least one busId.
         * NOTE: May not seem relevant to put in ShuttlePassenger. But ShuttlePassenger is the binding class for ShuttleSchedule and ShuttleBus.
         * </summary>
        */
        public List<string> RetrieveBusesInSameSchedule(string scheduleId);

        /*
         * <summary>
         * Returns the busId of all buses that are booked.
         * Returns a list of Strings. List will contain at least one busId.
         * NOTE: May not seem relevant to put in ShuttlePassenger. But ShuttlePassenger is the binding class for ShuttleSchedule and ShuttleBus.
         * </summary>
        */
        public List<string> RetrieveAllBookedBuses(DateTime dateTime);

    }
}
