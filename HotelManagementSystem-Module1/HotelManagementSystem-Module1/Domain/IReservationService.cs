using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
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
         * Delete Reservation if and only if Status is cancelled
         * </summary>
         * <param id = "id">Id of Reservation</param>
         * <returns>true if reservation deleted successfully</returns>
         */
        bool DeleteReservation(int id);

        /*
         * <summary>
         * Update Reservation
         * </summary>
         * <param Reservation = reservation>New Information of Reservation</param>
         * <returns>true if reservation updated successfully</returns>
         */
        public bool UpdateReservation(Reservation reservation);

        /*
         * <summary>
         * Get Reservation by Status (For Mod 2)
         * </summary>
         * <param status = status>status of Reservation</param>
         * <returns>List of Reservation that meets the status conditions</returns>
         */
        IEnumerable<Reservation> GetReservationByStatus(string status);
    }
}
