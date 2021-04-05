using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.PaymentInterfaces
{
    public interface iReceiptItem
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Quantity { get; set; }
        decimal Price { get; set; }
    }
}
