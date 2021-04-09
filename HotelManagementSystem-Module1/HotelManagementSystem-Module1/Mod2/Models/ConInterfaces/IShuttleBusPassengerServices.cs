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
    * IShuttleBusPassengerServices Interface
    */
    public interface IShuttleBusPassengerServices
    {

        public string GeneratePassengerID(string scheduleID, string passengerIndex);

        /*
         * <summary>
         * Adds shuttle passengers for a shuttle schedule
         * Will iterate through multiple buses to look for available seats
         * requires ID of the desired schedule, its schedule date time, direction and amt of passengers
         * </summary>
         * <returns>
         * returns bool upon adding all passengers into the database, successfully or otherwise
         * <returns>
        */
        public Task<bool> InsertShuttlePassengers(string scheduleId, DateTime dateTime, string direction, int numOfPassengers);

        /*
         * <summary>
         * Compiles a list of ShuttlePassengers based on the given ShuttleSchedule information
         * Runs through every existing ShuttleSchedule and ShuttleBus for the most efficient adding
         * </summary>
         * <returns>
         * returns a list of ShuttlePassengers
         * <returns>
        */
        public List<ShuttlePassenger> PrepareShuttlePassengers(string scheduleId, DateTime dateTime, string direction, int numOfPassengers);

        /*
         * <summary>
         * Checks for bus seats when provided a list of schedules,
         * a desired schedule date time, direction, and needed amount of seats
         * </summary>
         * <returns>
         * returns all the seats that buses under this schedule can provide
         * <returns>
        */
        public int GetBusSeatsAvailableForShuttleTiming(List<ShuttleSchedule> shuttleScheduleList, DateTime dateTime, string direction, int neededAmountOfSeats);

        /*
         * <summary>
         * Checks for the amount of available seats that booked buses (i.e existing shuttle schedules) can offer
         * </summary>
         * <returns>
         * returns all the seats that can be booked under this manner
         * <returns>
        */
        public int GetAvailableSeatCountFromBookedBus(DateTime dateTime, string direction, string busId, int neededAmountOfSeats);

        /*
         * <summary>
         * Checks for the amount of available seats that unbooked buses can offer
         * </summary>
         * <returns>
         * returns all the seats that can be booked under this manner
         * <returns>
        */
        public int GetBusSeatsAvailableFromUnbookedBuses(List<string> busIdList, int requiredSeats);

        /*
         * <summary>
         * Returns a list of ShuttlePassengers that are tied under a given shuttleScheduleId
         * </summary>
        */
        public List<ShuttlePassenger> GetShuttlePassengersOfShuttleSchedule(string shuttleScheduleId);

        /*
         * <summary>
         * Returns a list of Bus IDs (in string) that are tied under a given shuttleScheduleId
         * </summary>
        */
        public List<string> GetBusIdInSameSchedule(string shuttleScheduleId);

        /*
         * <summary>
         * Returns a list of ShuttlePassengers that are passengers of a given bus, at a given dateTime and direction
         * requires direction to be either 'Arrival' or 'Departure'
         * </summary>
        */
        public List<ShuttlePassenger> GetShuttlePassengersOfBus(string busId, DateTime scheduleDateTime, string direction);

    }
}
