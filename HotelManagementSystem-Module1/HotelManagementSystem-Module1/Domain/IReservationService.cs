using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;

/*
 * Owner of Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain
{
    public interface IReservationService
    {
        /*
         * <summary>
         * Get all Existing Reservations
         * </summary>
         * <returns>List of all Existing Reservations <returns>
         */
        IEnumerable<Reservation> GetAllReservations();

        /*
         * <summary>
         * Get Latest Inserted Data
         * </summary>
         * <returns>Latest Inserted Reservations row<returns>
         */
        Reservation GetLatestReservation();
        /*
         * <summary>
         * Search for Reservation by id
         * </summary>
         * <param id = "id">Id of Reservation</param>
         * <returns>Reservation matching with id</returns>
         */
        Reservation SearchByReservationId(int id);

        /*
         * <summary>
         * Search for Reservation by Guest Id
         * </summary>
         * <param id = "id">Id of Guest</param>
         * <returns>Reservation matching with Guest Id</returns>
         */
        IEnumerable<Reservation> SearchByGuestId(int id);

        /*
         * <summary>
         * Create Reservation
         * </summary>
         * <param Reservation = reservation>Information of new Reservation</param>
         * <returns>true if reservation created successfully</returns>
         */
        bool CreateReservation(Reservation reservation);

        /*
         * <summary>
         * Update Reservation Status only 
         * </summary>
         * <param id = "id">Id of Reservation</param>
         * <returns>true if reservation status updated successfully</returns>
         */
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

        /*
         * <summary>
         * Get Reservation by Status (For Mod 2 to pull reservations today with status provided)
         * </summary>
         * <param status = status>status of Reservation</param>
         * <returns>List of Reservation that meets the status conditions</returns>
         */
        IEnumerable<Reservation> GetTodayReservationByStatus(string status);

        /*
         * <summary>
         * Get Reservation Status by Date for Trend Analysis
         * </summary>
         * <param status = status>Status of Reservation</param>
         * <param start = start>Start date of Reservation</param>
         * <param end = end>End date of Reservation</param>
         * <returns>List of Reservation that meets the status and date conditions</returns>
         */
        IEnumerable<Reservation> GetReservationStatusByDate(string status, DateTime start, DateTime end);

        /*
         * <summary>
         * Delete Reservation if and only if Status is cancelled
         * </summary>
         * <param id = "id">Id of Reservation</param>
         * <returns>true if reservation deleted successfully</returns>
         */
        bool DeleteReservation(int id);
    }
}
