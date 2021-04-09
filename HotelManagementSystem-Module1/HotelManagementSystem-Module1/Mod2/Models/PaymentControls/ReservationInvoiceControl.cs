using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentInterfaces;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.PaymentInterfaces;
using IReservationService = HotelManagementSystem.Domain.IReservationService;
using Reservation = HotelManagementSystem.Domain.Models.Reservation;

/*
    * Author: Mod 2 Team 7
    * ReservationInvoiceControl Class 
*/

namespace HotelManagementSystem.Models.PaymentControls
{
    public class ReservationInvoiceControl : iReservationInvoice
    {
        private readonly iReservationInvoiceGateway _invoiceGateway;
        private readonly IReservationService _reservationservice;
        public ReservationInvoiceControl(iReservationInvoiceGateway invoiceGateway, IReservationService reservationservice)
        {
            _invoiceGateway = invoiceGateway;
            _reservationservice = reservationservice;
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

        public IEnumerable<ReservationInvoice> retrieveByGuestId(int id)
        {
            return _invoiceGateway.findByGuestID(id);
        }

        public bool addPayment(ReservationInvoice invoice)
        {
            return _invoiceGateway.insert(invoice);
        }

        public bool updatePayment(ReservationInvoice invoice)
        {
            return _invoiceGateway.update(invoice);
        }

        public bool deletePayment(int id)
        {
            var invoice = _invoiceGateway.findByID(id);
            return _invoiceGateway.delete(invoice);
        }

        public void notifyCancellation(int resID, decimal price)
        {
            PaymentFactory factory = PaymentFactory.getInstance();
            Reservation res = _reservationservice.SearchByReservationId(resID);
            int guestID = int.Parse(res.GetReservation()["guestID"].ToString());

            ReservationInvoice resInvoice =  
                factory.createPayment(guestID: guestID, resID: resID, type: "ReservationInvoice", 
                amount: price, status: "Outstanding", method: "");

            _invoiceGateway.insert(resInvoice);
        }
    }
}
