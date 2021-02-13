using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
{
    interface IFacilityReservationService
    {
        /// <summary>
        /// Get all facility reservations
        /// </summary>
        /// <returns>List of all facility reservations</returns>
        IEnumerable<FacilityReservation> RetrieveReservations();

        /// <summary>
        /// Make a new facility reservation
        /// </summary>
        /// <param name="facilityReservation">Information of new facility reservation</param>
        void MakeReservation(FacilityReservation facilityReservation);

        /// <summary>
        /// Updates an existing facility reservation's information
        /// </summary>
        /// <param name="facilityReservation">Updated information of existing facility reservation</param>
        void UpdateReservation(FacilityReservation facilityReservation);

        /// <summary>
        /// Deletes an existing facility reservation
        /// </summary>
        /// <param name="facilityReservation">FacilityReservation information to delete</param>
        void DeleteReservation(FacilityReservation facilityReservation);

        /// <summary>
        /// Deletes an existing facility reservation
        /// </summary>
        /// <param name="reservationId">ID of facility reservation to delete</param>
        void DeleteReservation(int reservationId);
        
        /// <summary>
        /// Checks if a facility reservation is valid
        /// </summary>
        /// <param name="facilityReservation">Information of facility reservation to check</param>
        void CheckValidReservation(FacilityReservation facilityReservation);
    }
}
