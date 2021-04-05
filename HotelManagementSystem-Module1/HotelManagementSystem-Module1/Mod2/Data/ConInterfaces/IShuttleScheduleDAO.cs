using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IShuttleScheduleDAO Interface
    */
    public interface IShuttleScheduleDAO
    {
        /*
         * <summary>
         * Add guest Shuttle Booking to the database
         * </summary>
         * <returns>
         * bool upon succesfully adding item to the database
         * <returns>
        */
        public Task<bool> InsertShuttleBooking(ShuttleSchedule shuttleShedule);

        /*
         * <summary>
         * Updates a guest Shuttle Booking in the database
         * </summary>
         * <returns>
         * bool upon succesfully updating the item in the database
         * <returns>
        */
        public Task<bool> UpdateShuttleBooking(ShuttleSchedule shuttleShedule);

        /*
        * <summary>
        * Retrieve all ShuttleSchedules
        * </summary>
        * <returns>
        * List of ShuttleSchedule Object
        * <returns>
       */
        public List<ShuttleSchedule> RetrieveAllShuttleBooking();

        /*
        * <summary>
        * Retrieve all ShuttleSchedule objects by ID
        * </summary>
        * <returns>
        * ShuttleSchedule Object
        * <returns>
        */
        public ShuttleSchedule RetrieveShuttleBookingByID(string id);

        /*
        * <summary>
        * Retrieve all ShuttleSchedule objects by DateTime
        * </summary>
        * <returns>
        * List<ShuttleSchedule>
        * <returns>
        */
        public List<ShuttleSchedule> RetrieveAllShuttleBookingByDateTime(DateTime dateTime);

        /*
        * <summary>
        * Retrieve al ShuttleSchedule object by direction
        * </summary>
        * <returns>
        * List<ShuttleSchedule>
        * <returns>
        */
        public List<ShuttleSchedule> RetrieveAllShuttleBookingByDirection(string direction);

        /*
        * <summary>
        * Retrieve all ShuttleSchedule objects by Date and Direction
        * </summary>
        * <returns>
        * List<ShuttleSchedule>
        * <returns>
        */
        public List<ShuttleSchedule> RetrieveAllShuttleBookingByDateAndDirection(DateTime dateTime, string direction);

    }
}
