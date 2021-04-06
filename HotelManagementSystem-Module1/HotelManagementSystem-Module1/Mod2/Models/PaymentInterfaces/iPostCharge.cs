using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Models.PaymentEntities;

namespace HotelManagementSystem.Models.PaymentInterfaces
{
    public interface iPostCharge : iPayment <PostCharge>
    {
        bool addItem(int guestID, string itemName, string itemDesc, int itemQnty, decimal itemPrice);
        void removeItem(int id, int itemID);
        decimal calculateTotal();
    }
}
