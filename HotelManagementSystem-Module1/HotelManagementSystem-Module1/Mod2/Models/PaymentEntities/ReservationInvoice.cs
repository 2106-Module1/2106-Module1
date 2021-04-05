using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.PaymentEntities
{
    public class ReservationInvoice : AbstractPayment
    {
        [Display(Name = "Reservation ID")]
        public int ReservationId { get; set; }

        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }
    }
}
