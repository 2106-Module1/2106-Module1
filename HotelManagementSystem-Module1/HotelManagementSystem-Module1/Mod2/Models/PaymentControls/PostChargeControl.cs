using System;
using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentInterfaces;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.PaymentInterfaces;

namespace HotelManagementSystem.Models.PaymentControls
{
    public class PostChargeControl : iPostCharge
    {
        private readonly iPostChargeGateway _chargeGateway;
        public PostChargeControl(iPostChargeGateway chargeGateway)
        {
            _chargeGateway = chargeGateway;
        }
        public IEnumerable<PostCharge> retrieveAll()
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }

        public PostCharge retrieveByID(int id)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
        public void addPayment(PostCharge payment)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
        public void updatePayment(PostCharge payment)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
        public void deletePayment(int id)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
        public bool addItem(int guestID, string itemName, string itemDesc, int itemQnty, decimal itemPrice)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
        public void removeItem(int id, int itemID)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
        public decimal calculateTotal()
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}
