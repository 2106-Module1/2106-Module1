using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Models.PaymentInterfaces;

namespace HotelManagementSystem.Models.PaymentEntities
{
    public class PostCharge : AbstractPayment
    {
        [Display(Name = "Item List")]
        public IList<ReceiptItem> ItemList { get; set; }
    }
}
