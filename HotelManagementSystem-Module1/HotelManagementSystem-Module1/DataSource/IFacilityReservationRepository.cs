using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.DataSource
{
    /// <summary>
    /// A repository interface for retrieving FacilityReservation entities from a database
    /// </summary>
    public interface IFacilityReservationRepository : IRepository<FacilityReservation>
    {
        /// <summary>
        /// Retrieves the reservations made by a specific reservee
        /// </summary>
        /// <param name="reserveeId">The guest id of the reservee</param>
        /// <returns>List of reservations made by the reservee</returns>
        IEnumerable<FacilityReservation> GetByReserveeId(int reserveeId);
    }
}
