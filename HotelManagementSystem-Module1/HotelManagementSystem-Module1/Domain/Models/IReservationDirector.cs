using System.Collections.Generic;

/*
 * Owner of IReservationDirector Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    public interface IReservationDirector
    {
        (Reservation, string) BuildNewReservation(IReservationBuilder builder, Dictionary<string, object> res);
    }
}
