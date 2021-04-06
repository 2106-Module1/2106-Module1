using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.Domain
{
    public interface IFacilityReservationService
    {
        /// <summary>
        /// Get all facility reservations
        /// </summary>
        /// <returns>List of all facility reservations</returns>
        IEnumerable<FacilityReservation> RetrieveReservations();

        /// <summary>
        /// Gets the facility reservations by id
        /// </summary>
        /// <returns>Facility reservations with the matching reservation id</returns>
        FacilityReservation RetrieveByReservationId(int reservationId);

        /// <summary>
        /// Get all facility reservations made by a specific reservee
        /// </summary>
        /// <returns>List of all facility reservations made by the specific reservee</returns>
        IEnumerable<FacilityReservation> RetrieveByReserveeId(int reserveeId);

        /// <summary>
        /// Make a new facility reservation
        /// </summary>
        /// <param name="facilityReservation">Information of new facility reservation</param>
        bool MakeReservation(FacilityReservation facilityReservation);

        /// <summary>
        /// Updates an existing facility reservation's information
        /// </summary>
        /// <param name="facilityReservation">Updated information of existing facility reservation</param>
        bool UpdateReservation(FacilityReservation facilityReservation);

        /// <summary>
        /// Deletes an existing facility reservation
        /// </summary>
        /// <param name="facilityReservation">FacilityReservation information to delete</param>
        bool DeleteReservation(FacilityReservation facilityReservation);

        /// <summary>
        /// Deletes an existing facility reservation
        /// </summary>
        /// <param name="reservationId">ID of facility reservation to delete</param>
        bool DeleteReservation(int reservationId);

        /// <summary>
        /// Checks if a facility reservation is valid
        /// </summary>
        /// <param name="facilityReservation">Information of facility reservation to check</param>
        bool CheckValidReservation(FacilityReservation facilityReservation);
    }
}
