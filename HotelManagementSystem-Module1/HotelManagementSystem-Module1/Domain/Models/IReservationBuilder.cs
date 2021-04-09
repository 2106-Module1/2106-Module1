using System;

/*
 * Owner of IReservationBuilder Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    public interface IReservationBuilder
    {
        /// <summary>
        /// Set the guest Id for a new reservation object
        /// </summary>
        /// <param name="guestId">The unique ID of the guest making a reservation</param>
        void GuestReservationBuilder(int guestId);
        
        /// <summary>
        /// Set the room type, number of guest and promo code for a new reservation object
        /// </summary>
        /// <param name="roomType">The room type request by the guest making a reservation</param>
        /// <param name="noOfGuest">The number of guest that will be staying in the room</param>
        /// <param name="promo">The promo code given for discount </param>
        void ReservationRoomBuilder(string roomType, int noOfGuest, string promo);
        
        /// <summary>
        /// Set the check in and check out date for a new reservation object
        /// </summary>
        /// <param name="start">The check in date for the reservation</param>
        /// <param name="end">The check out date for the reservation</param>
        void ReservationDatesBuilder(DateTime start, DateTime end);

        /// <summary>
        /// Set the remarks for a new reservation object
        /// </summary>
        /// <param name="remarks">The special request of the guest</param>
        void AdditionalPreferenceBuilder(string remarks);
        
        /// <summary>
        /// Set the status and modified date for a new reservation object
        /// </summary>
        /// <param name="status">The current status of the reservation</param>
        /// <param name="modDateTime">The date and time of when the record was last updated</param>
        void ReservationStateBuilder(string status, DateTime modDateTime);

        /// <summary>
        /// Check if reservation is acceptable to be registered as a new reservation record
        /// </summary>
        /// <return>true or false if reservation object can be built and error message string if there is</return>
        (bool, string) CanBuild();

        /// <summary>
        /// Get the newly created reservation object
        /// </summary>
        /// <return>A Reservation Object</return>
        Reservation GetNewReservation();
    }
}
