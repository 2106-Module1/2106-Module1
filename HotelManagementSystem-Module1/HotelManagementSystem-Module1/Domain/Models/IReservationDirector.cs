using System.Collections.Generic;

/*
 * Owner of IReservationDirector Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    public interface IReservationDirector
    {
        /// <summary>
        /// Update Reservation Status only 
        /// </summary>
        /// <param name="builder">builder object</param>
        /// <param name="res">Dictionary with details to create reservation object</param>
        /// <returns>Reservation object and a string if there is an error in the building of reservation object</returns>
        (Reservation, string) BuildNewReservation(IReservationBuilder builder, Dictionary<string, object> res);
    }
}
