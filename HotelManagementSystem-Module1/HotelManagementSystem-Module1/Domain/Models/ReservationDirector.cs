using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class ReservationDirector
    {
        public Reservation BuildNewReservation(IReservationBuilder builder, Dictionary<string, object> res)
        {
            builder.GuestReservationBuilder((int)res["guestID"]);
            builder.ReservationRoomBuilder((string)res["roomType"], (double)res["price"], (int)res["numOfGuest"]);
            builder.ReservationDatesBuilder((DateTime)res["start"], (DateTime)res["end"]);
            builder.ReservationStateBuilder("Unfulfilled", DateTime.Now);
            builder.AdditionalPreferenceBuilder((string)res["remark"], (string)res["promoCode"]);

            return builder.GetNewReservation();
        }
    }
}
