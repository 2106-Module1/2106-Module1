using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentEntities;

/*
    * Author: Mod 2 Team 7
    * iPostChargeGateway Interface 
*/

namespace HotelManagementSystem.Data.PaymentInterfaces
{
    public interface iPostChargeGateway
    {
        /*
         * <summary>
         * Retrieves all Post Charges from the database
         * </summary>
         * <returns>
         * An IEnumerable list of all PostCharge objects 
         * <returns>
        */
        IEnumerable<PostCharge> retrieveAllCharges();

        /*
         * <summary>
         * Retrieves all Post Charges by Guest ID
         * </summary>
         * <returns>
         * An IEnumerable list of PostCharge objects 
         * <returns>
        */
        IEnumerable<PostCharge> findByGuestID(int id);

        /*
         * <summary>
         * Retrieves a Post Charges by its Payment ID
         * </summary>
         * <returns>
         *A PostCharge object
         * <returns>
        */
        PostCharge findByID(int id);

        /*
        * <summary>
        * Inserts a PostCharge object into the database
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
        */
        bool insert(PostCharge PostCharge);

        /*
        * <summary>
        * Updates a PostCharge object in the database
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
        */
        bool update(PostCharge PostCharge);

        /*
        * <summary>
        * Deletes a PostCharge object from the database
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
        */
        bool delete(PostCharge PostCharge);

        /*
        * <summary>
        * Deletes a ReceiptItem object from the database
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
        */
        bool deleteReceiptItem(int receiptItemId);
    }
}
