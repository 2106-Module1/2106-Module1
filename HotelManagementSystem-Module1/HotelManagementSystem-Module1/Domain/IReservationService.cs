using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;

/*
 * Owner of IReservationService Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain
{
    public interface IReservationService
    {
        /// <summary>
        /// Get all Existing Reservations
        /// </summary>
        /// <returns>List of all Existing Reservations</returns>
        IEnumerable<Reservation> GetAllReservations();

        /// <summary>
        /// Get Latest Inserted Data
        /// </summary>
        /// <returns>Latest Inserted Reservations row</returns>
        Reservation GetLatestReservation();

        /// <summary>
        /// Search for Reservation by Reservation Id
        /// </summary>
        /// <param name="id">The Id of Reservation</param>
        /// <returns>Reservation matching with Reservation Id</returns>
        Reservation SearchByReservationId(int id);
        
        /// <summary>
        /// Search for Reservation by Guest Id
        /// </summary>
        /// <param name="id">The Id of Guest</param>
        /// <returns>Reservation matching with Guest Id</returns>
        IEnumerable<Reservation> SearchByGuestId(int id);

        /// <summary>
        /// Insert new reservation object into the database
        /// </summary>
        /// <param name="reservation">The new reservation object to insert into database</param>
        /// <returns>true if reservation created successfully</returns>
        bool CreateReservation(Reservation reservation);
        
        /// <summary>
        /// Update Reservation Status only 
        /// </summary>
        /// <param name="resId">Id of Reservation</param>
        /// <param name="status">New Status of Reservation</param>
        /// <returns>true if reservation status updated successfully</returns>
        bool UpdateReservationStatus(int resId, string status);

        /*
         * <summary>
         * Update Reservation
         * </summary>
         * <param Reservation = reservation>New Information of Reservation</param>
         * <returns>true if reservation updated successfully</returns>
         */
        bool UpdateReservation(int resId, int pax, string roomType, DateTime startDate, DateTime endDate,
            string remarks, DateTime modifiedDate, string promoCode, double price, string status);

        /// <summary>
        /// Function prepared for Mod 2 Team 1
        /// Search and retrieve reservation record by given status and by start date today
        /// </summary>
        /// <param name="status">The status of the reservation record</param>
        /// <returns>A Enumerable list of reservation objects by given status</returns>
        IEnumerable<Reservation> GetTodayReservationByStatus(string status);

        /// <summary>
        /// Search and retrieve reservation record by given status and by start and end date today
        /// </summary>
        /// <param name="status">The status of the reservation record</param>
        /// <param name="start">The start of the date range</param>
        /// <param name="end">The end of the date range</param>
        /// <returns>A Enumerable list of reservation records based on the parameters provided</returns>
        IEnumerable<Reservation> GetReservationStatusByDate(string status, DateTime start, DateTime end);

        /// <summary>
        /// Delete reservation record from the database
        /// </summary>
        /// <param name="id">The reservation Id of the reservation record</param>
        /// <returns>true, on successful delete</returns>
        bool DeleteReservation(int id);
    }
}
