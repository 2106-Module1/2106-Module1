using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentEntities;

/*
    * Author: Mod 2 Team 7
    * iReservationInvoiceGateway Interface 
*/

namespace HotelManagementSystem.Data.PaymentInterfaces
{
    public interface iReservationInvoiceGateway
    {
        /*
         * <summary>
         * Retrieves all Reservation Invoices from the database
         * </summary>
         * <returns>
         * An IEnumerable list of all ReservationInvoice objects 
         * <returns>
        */
        IEnumerable<ReservationInvoice> retrieveAllInvoices();


        /*
         * <summary>
         * Retrieves all Reservation Invoices by Reservation ID
         * </summary>
         * <returns>
         * An IEnumerable list of ReservationInvoice objects 
         * <returns>
        */
        IEnumerable<ReservationInvoice> findByResID(int id);

        /*
        * <summary>
        * Retrieves all Reservation Invoices by Guest ID
        * </summary>
        * <returns>
        * An IEnumerable list of ReservationInvoice objects 
        * <returns>
        */
        IEnumerable<ReservationInvoice> findByGuestID(int id);

       /*
        * <summary>
        * Retrieves a Reservation Invoice by its Payment ID
        * </summary>
        * <returns>
        * A ReservationInvoice object
        * <returns>
       */
        ReservationInvoice findByID(int id);

        /*
        * <summary>
        * Inserts a ReservationInvoice object into the database
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
        */
        bool insert(ReservationInvoice reservationInvoice);

        /*
        * <summary>
        * Updates a ReservationInvoice object in the database
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
        */
        bool update(ReservationInvoice reservationInvoice);

        /*
        * <summary>
        * Deletes a ReservationInvoice object from the database
        * </summary>
        * <returns>
        * A boolean flag to indicate if the operation is sucessful
        * <returns>
        */
        bool delete(ReservationInvoice reservationInvoice);
    }
}
