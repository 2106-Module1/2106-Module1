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
         * Requires the guestId of the booking guest
         * </summary>
         * <returns>
         * string
         * <returns>
        */
        public string GenerateID(int guestID, string direction);

        /*
         * <summary>
         * Request to add guest Shuttle Booking to the database
         * Requires a ShuttleSchedule object and a List<ShuttlePassenger.ReadOnly> object
         * Those objects should be acquired via the CheckAvailabilityForDateAndTime method
         * </summary>
         * <returns>
         * bool upon succesfully adding item to the database
         * <returns>
        */
        public Task<bool> AddGuestShuttleBooking(ShuttleSchedule shuttleSchedule);

        /*
         * <summary>
         * Domain logic to check for available seats by Date and direction
         * </summary>
         * <returns>
         * returns a tuple with 3 items.
         * Item 1 = boolean value that tracks if there is availability for the given slots and time. true = good schedule, false = rejected
         * Item 2 = ShuttleSchedule object to be added
         * Item 3 = List<ShuttlePassenger> list of passengers to be added
         * insert Item2 and Item 3 into AddGuestShuttleBooking if Item1 is true
         * reject if item1 is false
         * <returns>
        */
        public bool CheckAvailabilityForDateAndTime(DateTime dateTime, string direction, int numOfPassengers);

        /*
        * <summary>
        * Updates the state of a schedule to be "Cancelled"
        * requires the readonly shuttle object
        * </summary>
        * <returns>
        * returns true if successful, false if not
        * <returns>
       */
        public Task<bool> UpdateScheduleStateCancelled(ShuttleSchedule.ReadOnly shuttleSchedule);

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
