using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentEntities;

/*
    * Author: Mod 2 Team 7
    * iReservationInvoice Interface 
*/

namespace HotelManagementSystem.Models.PaymentInterfaces
{
    public interface iReservationInvoice : iPayment<ReservationInvoice>
    {
        /*
         * <summary>
         * Retrieves ReservationInvoice objects by Reservation ID from the gateway
         * </summary>
         * <returns>
         * An IEnumerable list of ReservationInvoice objects 
         * <returns>
        */
        IEnumerable<ReservationInvoice> getInvoiceByResID(int id);

        /*
         * <summary>
         * Creates a new Reservation Invoice to charge for cancelled reservations
         * </summary>
        */
        void notifyCancellation(int resID, decimal price);
    }
}
