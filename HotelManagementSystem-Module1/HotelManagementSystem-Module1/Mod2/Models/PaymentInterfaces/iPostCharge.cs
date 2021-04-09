using HotelManagementSystem.Models.PaymentEntities;

/*
    * Author: Mod 2 Team 7
    * iPostCharge Interface 
*/

namespace HotelManagementSystem.Models.PaymentInterfaces
{
    public interface iPostCharge : iPayment <PostCharge>
    {
        /*
         * <summary>
         * Add a Receipt Item to a Post Charge payment
         * </summary>
         * <returns>
         * A boolean flag to indicate if the operation is sucessful
         * <returns>
        */
        bool addItem(int guestID, string itemName, string itemDesc, int itemQnty, decimal itemPrice);

        /*
         * <summary>
         * Removes a Receipt Item from a Post Charge payment
         * </summary>
         * <returns>
         * A boolean flag to indicate if the operation is sucessful
         * <returns>
        */
        bool removeReceiptItem(int paymentId, int deleteReceiptItem);

        /*
         * <summary>
         * Calculates the total price of all Receipt Items in a Post Charge
         * </summary>
         * <returns>
         * A decimal value of the total price
         * <returns>
        */
        decimal calculateTotal(int paymentId);

        /*
         * <summary>
         * Sends a reminder to all guests that have unpaid outstanding payments
         * </summary>
        */
        void remindOutstanding();

        /*
         * <summary>
         * Sends a payment reminder email to a guest
         * </summary>
         * <returns>
         * A boolean flag to indicate if the operation is sucessful
         * <returns>
        */
        bool sendEmail(string guestEmail, decimal amount);
    }
}
