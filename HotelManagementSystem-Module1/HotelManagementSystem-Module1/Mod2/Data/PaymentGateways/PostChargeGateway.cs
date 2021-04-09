using System.Collections.Generic;
using System.Linq;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.Mod2Repository;
using HotelManagementSystem.Data.PaymentInterfaces;
using Microsoft.EntityFrameworkCore;

/*
    * Author: Mod 2 Team 7
    * PostChargeGateway Class 
*/

namespace HotelManagementSystem.Data.PaymentGateways
{
    public class PostChargeGateway : iPostChargeGateway
    {
        private readonly Mod2Context _context;

        public PostChargeGateway(Mod2Context context)
        {
            _context = context;
        }

        public IEnumerable<PostCharge> retrieveAllCharges()
        {
            var Charges = from i in _context.PostCharge
                          .Include(b => b.ItemList)
                          select i;

            return Charges.ToList();
        }

        public PostCharge findByID(int id)
        {
            var PostCharge = _context.PostCharge
                .Include(b => b.ItemList)
                .FirstOrDefault(i => i.Id == id);

            return PostCharge;
        }

        public IEnumerable<PostCharge> findByGuestID(int id)
        {
            var Charges = from i in _context.PostCharge
                          .Include(b => b.ItemList)
                          where i.GuestId.Equals(id)
                           select i;

            return Charges.ToList();
        }

        public bool insert(PostCharge PostCharge)
        {
            _context.Add(PostCharge);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool update(PostCharge PostCharge)
        {
            _context.Update(PostCharge);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool delete(PostCharge PostCharge)
        {
            _context.PostCharge.Remove(PostCharge);
            return _context.SaveChanges() > 0 ? true : false;
        }
        public bool deleteReceiptItem(int receiptItemId)
        {
            var receiptItem = _context.ReceiptItem.SingleOrDefault(x => x.Id == receiptItemId); //returns a single item.

            if (receiptItem != null)
            {
                _context.ReceiptItem.Remove(receiptItem);
                return _context.SaveChanges() > 0 ? true : false;
            }
            return false;
        }
    }
}
