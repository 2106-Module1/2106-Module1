using System.Collections.Generic;

namespace HotelManagementSystem.Domain.Models
{
    public interface IReservationDirector
    {
        Reservation BuildNewReservation(IReservationBuilder builder, Dictionary<string, object> res);
    }
}
