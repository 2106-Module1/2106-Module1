
using System.Collections.Generic;

/*
    * Author: Mod 2 Team 7
    * iPayment Template Interface 
*/

namespace HotelManagementSystem.Models.PaymentInterfaces
{
    public interface iPayment<TPayment>
    {
        /*
         * <summary>
         * Retrieves all payment objects from the gateway
         * </summary>
         * <returns>
         * An IEnumerable list of all payment objects 
         * <returns>
        */
        IEnumerable<TPayment> retrieveAll();

        /*
         * <summary>
         * Retrieves a payment object by its payment ID from the gateway
         * </summary>
         * <returns>
         * An payment object
         * <returns>
        */
        TPayment retrieveByID(int id);

        /*
         * <summary>
         * Retrieves payment objects by guest ID from the gateway
         * </summary>
         * <returns>
         * An IEnumerable list of payment objects 
         * <returns>
        */
        IEnumerable<TPayment> retrieveByGuestId(int id);

        /*
         * <summary>
         * Adds a payment object via the gateway
         * </summary>
         * <returns>
         * A boolean flag to indicate if the operation is sucessful
         * <returns>
        */
        bool addPayment(TPayment payment);

        /*
        * <summary>
        * Updates a payment object via the gateway
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
       */
        bool updatePayment(TPayment payment);

        /*
        * <summary>
        * Deletes a payment object via the gateway
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
       */
        bool deletePayment(int id);
    }
}
