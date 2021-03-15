using System;

/*
 * Owner of IReservationBuilder Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    public interface IReservationBuilder
    {
        /*
         * <summary>
         * Set the guest Id for a new reservation object
         * </summary>
         * <param>guest id, the unique ID of the guest making a reservation </param>
         */
        void GuestReservationBuilder(int guestId);

        /*
         * <summary>
         * Set the room type, number of guest and promo code for a new reservation object
         * </summary>
         * <param>roomType, the room type request by the guest making a reservation
         * noOfGuest, the number of guest that will be staying in the room,
         * promo, the promo code given for discount </param>
         */
        void ReservationRoomBuilder(string roomType, int noOfGuest, string promo);

        /*
         * <summary>
         * Set the check in and check out date for a new reservation object
         * </summary>
         * <param>start, the check in date for the reservation,
         * end, the check out date for the reservation </param>
         */
        void ReservationDatesBuilder(DateTime start, DateTime end);

        /*
         * <summary>
         * Set the remarks for a new reservation object
         * </summary>
         * <param>remarks, special request of the guest</param>
         */
        void AdditionalPreferenceBuilder(string remarks);

        /*
         * <summary>
         * Set the status and modified date for a new reservation object
         * </summary>
         * <param>status, the current status of the reservation,
         * modDateTime, date and time of when the record was last updated </param>
         */
        void ReservationStateBuilder(string status, DateTime modDateTime);

        /*
         * <summary>
         * Check if reservation is acceptable to be registered as a new reservation record
         * </summary>
         * <return>true, if there is no illegal attribute in the reservation object based on the validation model</return>
         */
        bool CanBuild();

        /*
         * <summary>
         * Get the newly created reservation object
         * </summary>
         * <return>A reservation object</return>
         */
        Reservation GetNewReservation();
    }
}
