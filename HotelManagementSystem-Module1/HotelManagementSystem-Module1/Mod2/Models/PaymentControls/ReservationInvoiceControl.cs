using System;
using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentInterfaces;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.PaymentInterfaces;

namespace HotelManagementSystem.Models.PaymentControls
{
    public class ReservationInvoiceControl : iReservationInvoice
    {
        private readonly iReservationInvoiceGateway _invoiceGateway;
        public ReservationInvoiceControl(iReservationInvoiceGateway invoiceGateway)
        {
            _invoiceGateway = invoiceGateway;
        }

        public IEnumerable<ReservationInvoice> retrieveAll()
        {
            return _invoiceGateway.retrieveAllInvoices();
        }

        public IEnumerable<ReservationInvoice> getInvoiceByResID(int id)
        {
            return _invoiceGateway.findByResID(id);
        }

        public ReservationInvoice retrieveByID(int id)
        {
            return _invoiceGateway.findByID(id);
        }

        public void addPayment(ReservationInvoice invoice)
        {
            _invoiceGateway.insert(invoice);
        }

        public void updatePayment(ReservationInvoice invoice)
        {
            //TODO: Data validation
            _invoiceGateway.update(invoice);
        }

        public void deletePayment(int id)
        {
            var invoice = _invoiceGateway.findByID(id);
            _invoiceGateway.delete(invoice);
        }

        public void notifyCancellation(int resID, decimal price)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
