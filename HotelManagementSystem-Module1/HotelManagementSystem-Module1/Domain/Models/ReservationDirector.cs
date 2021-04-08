using System;
using System.Collections.Generic;

/*
 * Owner of ReservationDirector: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    public class ReservationDirector : IReservationDirector
    {
        public (Reservation, string) BuildNewReservation(IReservationBuilder builder, Dictionary<string, object> res)
        {
            builder.GuestReservationBuilder(Convert.ToInt32(res["guestID"]));
            builder.ReservationDatesBuilder(Convert.ToDateTime(res["start"]), Convert.ToDateTime(res["end"]));
            builder.ReservationRoomBuilder(Convert.ToString(res["roomType"]), Convert.ToInt32(res["numOfGuest"]), 
                Convert.ToString(res["promoCode"]));
            builder.AdditionalPreferenceBuilder(Convert.ToString(res["remark"]));
            builder.ReservationStateBuilder("Unfulfilled", DateTime.Now);

            if (builder.CanBuild().Item1)
            {
                return (builder.GetNewReservation(), "");
            }
            else
            {
                return (null, builder.CanBuild().Item2);
            }
        }
    }
}
