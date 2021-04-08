using System.Collections.Generic;
using System.Linq;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.Mod2Repository;
using HotelManagementSystem.Data.PaymentInterfaces;

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
                           select i;

            return Charges.ToList();
        }

        public PostCharge findByID(int id)
        {
            var PostCharge = _context.PostCharge
                .FirstOrDefault(i => i.Id == id);

            return PostCharge;
        }

        public void insert(PostCharge PostCharge)
        {
            _context.Add(PostCharge);
            _context.SaveChanges();
        }

        public void update(PostCharge PostCharge)
        {
            _context.Update(PostCharge);
            _context.SaveChanges();
        }

        public void delete(PostCharge PostCharge)
        {
            _context.PostCharge.Remove(PostCharge);
            _context.SaveChanges();
        }
    }
}
