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
    * IShuttleServices Interface
    */
    public interface IShuttleServices
    {
        /*
         * <summary>
         * Generates and returns a combined String that represents the true shuttle scheduleId
         * Requires the ScheduleDateTime and ScheduleId
         * </summary>
         * <returns>
         * string
         * <returns>
        */
        public string GenerateID(DateTime datetime, int guestID);

        /*
         * <summary>
         * Request to add guest Shuttle Booking to the database
         * Requires a ShuttleSchedule.ReadOnly object.
         * </summary>
         * <returns>
         * bool upon succesfully adding item to the database
         * <returns>
        */
        public Task<bool> AddGuestShuttleBooking(ShuttleSchedule.ReadOnly shuttleSchedule);

        /*
         * <summary>
         * Domain logic to check for available seats by Date and direction
         * </summary>
         * <returns>
         * returns TRUE if numOfPassenger is less than/equal to available amount of seats
         * returns FALSE if it exceeds
         * <returns>
        */
        public Task<bool> CheckAvailabilityForDateAndTime(DateTime Date, string direction, int numOfPassengers);

        /*
         * <summary>
         * Returns a list of all shuttle schedules in the system.
         * </summary>
         * <returns>
         * returns a List<ShuttleSchedule> object
         * <returns>
        */
        public List<ShuttleSchedule> GetAllShuttleSchedules();

        /*
         * <summary>
         * Returns a list of all shuttle passengers in the system that have a given shuttle schedule Id.
         * </summary>
         * <returns>
         * returns a List<ShuttlePassenger> object
         * <returns>
        */
        public List<ShuttlePassenger> GetShuttlePassengersOfShuttleSchedule(string shuttleScheduleId);

        /*
         * <summary>
         * Returns a ShuttleSchedule object with a given shuttle schedule Id.
         * As the scheduleId is a primary key, that should only be one intended result.
         * </summary>
         * <returns>
         * returns a ShuttleSchedule object. Returns null if the ID does not match existing shuttle schedule objects.
         * <returns>
        */
        public ShuttleSchedule GetShuttleScheduleById(string shuttleScheduleId);

    }
}
