using System;
using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Models.PaymentInterfaces;

namespace HotelManagementSystem.Models.PaymentEntities
{
    public class ReceiptItem : iReceiptItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
