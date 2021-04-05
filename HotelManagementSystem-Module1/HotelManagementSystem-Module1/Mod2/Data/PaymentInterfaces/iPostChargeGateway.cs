using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Models.PaymentEntities;

namespace HotelManagementSystem.Data.PaymentInterfaces
{
    public interface iPostChargeGateway
    {
        IEnumerable<PostCharge> retrieveAllCharges();
        PostCharge findByID(int id);
        void insert(PostCharge PostCharge);
        void update(PostCharge PostCharge);
        void delete(PostCharge PostCharge);
    }
}
