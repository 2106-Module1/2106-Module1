using System;
using System.ComponentModel.DataAnnotations;

/*
    * Author: Mod 2 Team 7
    * ReservationInvoice Class 
*/

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

    public enum ReservationInvoiceStatusType
    {
        Paid,
        Pending,
        Outstanding
    }
}
