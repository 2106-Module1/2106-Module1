using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Domain.Models
{
    public class ReservationDirector
    {
        public Reservation BuildNewReservation(IReservationBuilder builder, Dictionary<string, object> res)
        {
            builder.GuestReservationBuilder(Convert.ToInt32(res["guestID"]));
            builder.ReservationDatesBuilder(Convert.ToDateTime(res["start"]), Convert.ToDateTime(res["end"]));
            builder.ReservationRoomBuilder(Convert.ToString(res["roomType"]), Convert.ToInt32(res["numOfGuest"]), Convert.ToString(res["promoCode"]));
            builder.AdditionalPreferenceBuilder(Convert.ToString(res["remark"]));
            builder.ReservationStateBuilder("Unfulfilled", DateTime.Now);

            if (builder.CanBuild())
            {
                return builder.GetNewReservation();
            }
            else
            {
                return null;
            }
        }
    }
}
