using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain
{
    public interface IGuestService
    {
        /// <summary>
        /// Search for guests by name
        /// </summary>
        /// <param name="name">Name of guests</param>
        /// <returns>List of guests with matching names</returns>
        IEnumerable<Guest> SearchByGuestName(string name);

        /// <summary>
        /// Search for guests by id
        /// </summary>
        /// <param guestId="guestId">Id of guest</param>
        /// <returns>Guests with matching id</returns>
        Guest SearchByGuestId(int guestId);

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
        /// <returns>Has guest been successfully registered</returns>
        bool RegisterGuest(Guest guest);

        /// <summary>
        /// Updates an existing guest's information
        /// </summary>
        /// <param name="guest">Updated information of existing guest</param>
        /// <returns>Has guest been successfully updated</returns>
        bool UpdateGuest(Guest guest);

        /// <summary>
        /// Deletes an existing guest's information
        /// </summary>
        /// <param name="guest">Guest information to delete</param>
        /// <returns>Has guest been successfully deleted</returns>
        bool DeleteGuest(Guest guest);

        /// <summary>
        /// Deletes an existing guest's information
        /// </summary>
        /// <param name="guest">ID of guest to delete</param>
        /// <returns>Has guest been successfully deleted</returns>
        bool DeleteGuest(int guestId);

        /// <summary>
        /// Checks an existing guest's outstanding charges
        /// <param name="guest"">ID of guest to check for outstanding charges</param>
        /// <returns>Guest has outstanding charges</returns>
        /// </summary>
        /// 
        bool SearchOutstandingCharges(int outstandingCharges);
    }
}
