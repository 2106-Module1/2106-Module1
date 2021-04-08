using System;
using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentEntities;

namespace HotelManagementSystem.Models.PaymentInterfaces
{
    public interface iReservationInvoice : iPayment<ReservationInvoice>
    {
        IEnumerable<ReservationInvoice> getInvoiceByResID(int id);
        void notifyCancellation(int resID, decimal price);
    }
}
