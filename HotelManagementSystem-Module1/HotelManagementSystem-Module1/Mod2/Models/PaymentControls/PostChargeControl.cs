using System;
using System.Collections.Generic;
using HotelManagementSystem.Models.PaymentInterfaces;
using HotelManagementSystem.Models.PaymentEntities;
using HotelManagementSystem.Data.PaymentInterfaces;
using System.Linq;
using System.Net.Mail;
using System.Net;
using IGuestService = HotelManagementSystem.Domain.IGuestService;
using Guest = HotelManagementSystem.Domain.Models.Guest;

/*
    * Author: Mod 2 Team 7
    * PostChargeControl Class 
*/

namespace HotelManagementSystem.Models.PaymentControls
{
    public class PostChargeControl : iPostCharge
    {
        private readonly iPostChargeGateway _chargeGateway;
        private readonly IGuestService _guestService;
        public PostChargeControl(iPostChargeGateway chargeGateway, IGuestService guestService)
        {
            _chargeGateway = chargeGateway;
            _guestService = guestService;
        }
        public IEnumerable<PostCharge> retrieveAll()
        {
            return _chargeGateway.retrieveAllCharges();
        }

        public IEnumerable<PostCharge> retrieveByGuestId(int id)
        {
            return _chargeGateway.findByGuestID(id);
        }

        public PostCharge retrieveByID(int id)
        {
            return _chargeGateway.findByID(id);
        }
        public bool addPayment(PostCharge payment)
        {
            if (payment != null)
                return _chargeGateway.insert(payment);
            return false;
        }
        public bool updatePayment(PostCharge payment)
        {
            if (payment != null)
                return _chargeGateway.update(payment);
            return false;
        }
        public bool deletePayment(int id)
        {
            var postCharge = _chargeGateway.findByID(id);
            if (postCharge != null)
                return _chargeGateway.delete(postCharge);
            return false;
        }
        public bool addItem(int guestID, string itemName, string itemDesc, int itemQnty, decimal itemPrice)
        {
            PostCharge postCharge = null;
            var tempCharge = _chargeGateway.findByGuestID(guestID).FirstOrDefault();

            if (tempCharge == null)
            {
                PaymentFactory factory = PaymentFactory.getInstance();
                postCharge = factory.createPayment(guestID: guestID, type: "PostCharge",
                amount: itemPrice * itemQnty, status: "Pending", method: "");
                _chargeGateway.insert(postCharge);
            } 
            else
            {
                postCharge = tempCharge;
            }

            var item = new ReceiptItem();
            item.Name = itemName;
            item.Description = itemDesc;
            item.Quantity = itemQnty;
            item.Price = itemPrice;

            try {
                postCharge.ItemList.Add(item);
                _chargeGateway.update(postCharge);
                return true;
            } catch (Exception e) {
                return false;
            }
           
        }
        public bool removeReceiptItem(int paymentId, int receiptItemId)
        {
            var postCharge = _chargeGateway.findByID(paymentId);
            if (postCharge == null)
                return false;

            return _chargeGateway.deleteReceiptItem(receiptItemId);
        }
        public decimal calculateTotal(int paymentId)
        {
            decimal paymentCharge = 0;
            decimal receiptItemCharge = 0;
            var paymentEntity = _chargeGateway.findByID(paymentId);
            paymentCharge = paymentEntity.Amount;
            receiptItemCharge = paymentEntity.ItemList.Select(i => i.Price).Sum();

            return paymentCharge + receiptItemCharge;
        }

        public void remindOutstanding()
        {
            IEnumerable<PostCharge> allCharges = _chargeGateway.retrieveAllCharges();
            foreach (PostCharge charge in allCharges)
            { 
                if (charge.Status == "Outstanding")
                { 
                    Guest guest = _guestService.SearchByGuestId(charge.GuestId);
                    decimal amount = charge.Amount;
                    string email = guest.EmailDetails();
                    sendEmail(email, amount);
                }
            }
        }

        public bool sendEmail(string guestEmail, decimal amount)
        {
            try {
                string senderMail = "support@hotelmanagement.com";
                string senderPass = "*****";
                string MailHostServer = "*******";
                string port = "587";
                string displayName = "Outstanding Payment Reminder";

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderMail, displayName);

                mail.To.Add(guestEmail);
                mail.Subject = displayName;

                mail.IsBodyHtml = true;
                mail.Body = "This email is a reminder that you have an outstanding payment of $" + amount.ToString() +
                    ". Kindly disregard this email if payment has already been made. Thank you.";

                mail.Priority = MailPriority.High;

                var stmp = new SmtpClient();
                stmp.Credentials = new NetworkCredential(senderMail, senderPass);
                stmp.Host = MailHostServer;
                stmp.Port = Convert.ToInt32(port);
                stmp.EnableSsl = true;
                stmp.Send(mail);
                return true;
            }
            catch (Exception e) { 
                return false;
            }
        }
    }
}
