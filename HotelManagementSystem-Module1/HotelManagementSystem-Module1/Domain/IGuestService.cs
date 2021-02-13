using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
{
    interface IGuestService
    {
        /// <summary>
        /// Search for guests by name
        /// </summary>
        /// <param name="name">Name of guests</param>
        /// <returns>List of guests with matching names</returns>
        IEnumerable<Guest> SearchByGuestName(string name);

        /// <summary>
        /// Search for guests by passport number
        /// </summary>
        /// <param name="passportNumber">Passport number of guests</param>
        /// <returns>List of guests with matching passport numbers</returns>
        IEnumerable<Guest> SearchByGuestPassportNumber(string passportNumber);

        /// <summary>
        /// Get all registered guests
        /// </summary>
        /// <returns>List of all registered guests</returns>
        IEnumerable<Guest> RetrieveGuests();

        /// <summary>
        /// Register a new guest
        /// </summary>
        /// <param name="guest">Information of new guest</param>
        void RegisterGuest(Guest guest);

        /// <summary>
        /// Updates an existing guest's information
        /// </summary>
        /// <param name="guest">Updated information of existing guest</param>
        void UpdateGuest(Guest guest);

        /// <summary>
        /// Deletes an existing guest's information
        /// </summary>
        /// <param name="guest">Guest information to delete</param>
        void DeleteGuest(Guest guest);

        /// <summary>
        /// Deletes an existing guest's information
        /// </summary>
        /// <param name="guest">ID of guest to delete</param>
        void DeleteGuest(int guestId);
    }
}
