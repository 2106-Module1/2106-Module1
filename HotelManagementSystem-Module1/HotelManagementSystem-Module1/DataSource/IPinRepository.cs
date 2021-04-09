using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.DataSource
{
    public interface IPinRepository
    {
        /// <summary>
        /// Save the updated Pin
        /// </summary>
        /// <param name="modifiedPin">Pin object with modified pin number</param>
        void UpdatePin(Pin modifiedpin);

        /// <summary>
        /// Retrieve Pin from database
        /// </summary>
        /// <returns>Pin</returns>
        Pin GetPin();

        /// <summary>
        /// Retrieve pin that has the exact same pin number in database
        /// </summary>
        /// <param name="pinNumber">pin number to match against record in database</param>
        /// <returns>Pin or null</returns>
        Pin ValidatePin(string pin);
    }
}
