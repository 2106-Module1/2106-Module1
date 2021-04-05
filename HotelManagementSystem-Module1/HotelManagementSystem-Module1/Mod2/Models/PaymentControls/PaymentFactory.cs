using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.PaymentInterfaces;

namespace HotelManagementSystem.Models.PaymentControls
{
    public class PaymentFactory
    {
        private static PaymentFactory instance;

        public static PaymentFactory getInstance()
        {
            if (instance == null)
            {
                instance = new PaymentFactory();
            }
            return instance;
        }

        public dynamic createPayment(int guestID, string type, string method, decimal amount, string status, int resID = -1)
        {
            if (type == "PostCharge") 
            {
                PostCharge charge = new PostCharge();
                charge.GuestId = guestID;
                charge.PaymentMethod = method;
                charge.Amount = amount;
                charge.Status = status;
                return charge;
            } 
            else if (type == "ReservationInvoice")
            {
                //TODO: Data validation
                ReservationInvoice invoice = new ReservationInvoice();
                invoice.ReservationId = resID;
                invoice.GuestId = guestID;
                invoice.IssueDate = DateTime.Now;
                invoice.PaymentMethod = method;
                invoice.Amount = amount;
                invoice.Status = status;
                return invoice;
            }
            else
            {
                //TODO: Error handling
                return null;
            }
        }
    }
}
