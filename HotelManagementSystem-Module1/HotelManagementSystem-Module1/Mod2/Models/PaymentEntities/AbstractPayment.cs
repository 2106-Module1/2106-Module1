using System.ComponentModel.DataAnnotations;

/*
    * Author: Mod 2 Team 7
    * AbstractPayment Class 
*/

namespace HotelManagementSystem.Models.PaymentEntities
{
    public abstract class AbstractPayment
    {
        public int Id { get; set; }

        [Display(Name = "Guest ID")]
        public int GuestId { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

    }
}
