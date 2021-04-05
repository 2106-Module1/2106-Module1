using System;

/*
 * Owner of IReservationValidator Interface: Mod 1 Team 4
 * To Validate Reservation object creation process
 */
namespace HotelManagementSystem.Domain
{
    public interface IReservationValidator
    {
        /*
         * <summary>
         * Check if the given guest id exist in the database
         * </summary>
         * <param>guest id, the unique ID of a guest in the database/param>
         * <returns>true if guest id exists and false if doesn't<returns>
         */
        bool ValidateGuest(int guestId);

        /*
         * <summary>
         * Check if the given promo code exist in the database
         * </summary>
         * <param>promo, the string of the promo code </param>
         * <returns>true if promo code exists and false if doesn't<returns>
         */
        bool ValidatePromo(string promo);

        /*
         * <summary>
         * Check if the given room type exist in the database
         * </summary>
         * <returns>true if room type exists and false if doesn't<returns>
         */
        bool ValidateRoomType(string roomType);

        /*
         * <summary>
         * Get the price of the room for the stay
         * </summary>
         * <param>roomType, the type of room requested,
         * days, the number of days guest reserves. </param>
         * <returns>true if promo code exists and false if doesn't<returns>
         */
        double GetRoomPrice(string roomType, int days);

        /*
         * <summary>
         * Get the price of the room for the stay if they have a promo code
         * </summary>
         * <param>roomType, the type of room requested,
         * days, the number of days guest reserves,
         * promo, the promo code string </param>
         * <returns>the price of the room in double data type<returns>
         */
        double GetDiscountPrice(string roomType, int days, string promo);

        /*
         * <summary>
         * Check if the number of guest entered meets the room capacity criteria
         * </summary>
         * <param>roomType, the type of room requested,
         * numofGuest, the number of guest staying </param>
         * <returns>true if the numOfGuest is below or equal to the room capacity<returns>
         */
        bool RoomTypeToGuestNum(string roomType, int numOfGuest);

        /*
         * <summary>
         * Check if the dates provide are valid
         * </summary>
         * <param>start, the check in date of the guest,
         * end, the check out date of the guest </param>
         * <returns>true, if the dates are valid<returns>
         */
       int CheckDates(DateTime start, DateTime end);

        /*
         * <summary>
         * Calculate the number of days the guest has reserved
         * </summary>
         * <param>start, the check in date of the guest,
         * end, the check out date of the guest </param>
         * <returns>the number of days the guest reserve<returns>
         */
        int NumOfDays(DateTime start, DateTime end);

        /*
         * <summary>
         * check if the guest needs to pay cancellation fee
         * </summary>
         * <param>dateCancelled, the date the guest cancel the reservation,
         * end,  the check in date of the guest </param>
         * <returns>true if guest needs to pay and false if no payment required<returns>
         */
        bool CheckCancellationFee(DateTime dateCancelled, DateTime start);
    }
}
