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
    /// A repository interface for retrieving Guest entities from a database
    /// </summary>
    public interface IGuestRepository : IRepository<Guest>
    {
        /// <summary>
        /// Retrieves all guests with a matching name
        /// </summary>
        /// <param name="name">The name of the guest to compare to</param>
        /// <returns>List of guests with matching names</returns>
        IEnumerable<Guest> GetByName(string name);

        /// <summary>
        /// Retrieves all guests with a passport number
        /// </summary>
        /// <param passportNumber="passportNumber">The passport number of the guest to compare to</param>
        /// <returns>List of guests with matching passport numbers</returns>
        IEnumerable<Guest> GetByPassportNumber(string passportNumber);
    }
}
