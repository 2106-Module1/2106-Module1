using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;

/*
 * Owner of Reservation Repository Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.DataSource
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        /*
         * <summary>
         * Search and retrieve reservation record by guest Id
         * </summary>
         * <param>guest id, the unique ID of a guest in the database </param>
         * <returns>A Enumerable list of reservation records tagged to guest Id<returns>
         */
        IEnumerable<Reservation> GetByGuestId(int id);

        /*
         * <summary>
         * Search and retrieve reservation record by given status and by start date today
         * </summary>
         * <param>status, the status of the reservation record </param>
         * <returns>A Enumerable list of reservation records tagged to guest Id<returns>
         */
        IEnumerable<Reservation> GetByTodayReservations(string status);

        /*
         * <summary>
         * Search and retrieve reservation record by given status and by start and end date today
         * </summary>
         * <param>status, the status of the reservation record,
         * start, the start date of the reservation,
         * end, the end date of the reservation</param>
         * <returns>A Enumerable list of reservation records tagged to guest Id<returns>
         */
        IEnumerable<Reservation> GetStatusByDate(string status, DateTime start, DateTime end);

        /*
         * <summary>
         * Get the latest inserted reservation record
         * </summary>
         * <returns>A reservation object<returns>
         */
        Reservation GetLatest();
    }
}
