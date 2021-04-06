using System.Collections.Generic;
using System.Linq;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.Mod2Repository;
using HotelManagementSystem.Data.PaymentInterfaces;

namespace HotelManagementSystem.Data.PaymentGateways
{
    public class ReservationInvoiceGateway : iReservationInvoiceGateway
    {
        private readonly Mod2Context _context;

        public ReservationInvoiceGateway(Mod2Context context)
        {
            _context = context;
        }

        public IEnumerable<ReservationInvoice> retrieveAllInvoices()
        {
            var invoices = from i in _context.ReservationInvoice
                           select i;

            return invoices.ToList();
        }

        public IEnumerable<ReservationInvoice> findByResID(int id)
        {
            var invoices = from i in _context.ReservationInvoice
                           where i.ReservationId.Equals(id)
                           select i;

            return invoices.ToList();
        }

        public ReservationInvoice findByID(int id)
        {
            var reservationInvoice = _context.ReservationInvoice
                .FirstOrDefault(i => i.Id == id);

            return reservationInvoice;
        }

        public void insert(ReservationInvoice reservationInvoice)
        { 
            _context.Add(reservationInvoice);
            _context.SaveChanges();
        }

        public void update(ReservationInvoice reservationInvoice)
        {
            _context.Update(reservationInvoice);
            _context.SaveChanges();
        }

        public void delete(ReservationInvoice reservationInvoice)
        {
            _context.ReservationInvoice.Remove(reservationInvoice);
            _context.SaveChanges();
        }
    }
}
