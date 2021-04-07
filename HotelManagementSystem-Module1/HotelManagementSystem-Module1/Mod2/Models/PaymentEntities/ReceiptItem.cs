using System;
using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Models.PaymentInterfaces;

/*
    * Author: Mod 2 Team 7
    * ReceiptItem Class 
*/

namespace HotelManagementSystem.Models.PaymentEntities
{
    public class ReceiptItem : iReceiptItem
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }
    }
}
