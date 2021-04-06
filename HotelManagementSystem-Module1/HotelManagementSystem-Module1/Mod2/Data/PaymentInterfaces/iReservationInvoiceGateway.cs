using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Models.PaymentEntities;

namespace HotelManagementSystem.Data.PaymentInterfaces
{
    public interface iReservationInvoiceGateway
    {
        IEnumerable<ReservationInvoice> retrieveAllInvoices();

        IEnumerable<ReservationInvoice> findByResID(int id);

        ReservationInvoice findByID(int id);

        void insert(ReservationInvoice reservationInvoice);

        void update(ReservationInvoice reservationInvoice);

        void delete(ReservationInvoice reservationInvoice);
    }
}
