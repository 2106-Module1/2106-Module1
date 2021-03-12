using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class NewReservation
    {
        [Key]
        private int ReservationId { get; set; }

        [Required(ErrorMessage = "Reservee Guest ID is required.")]
        private int ReserveGuestId { get; set; }

        [Required(ErrorMessage = "Number of Guest is required.")]
        private int NumOfGuest { get; set; }

        [Required(ErrorMessage = "Room Type is required.")]
        private string RoomType { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        private DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        private DateTime EndDate { get; set; }

        private string Remark { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        private DateTime LastModified { get; set; }

        private string PromoCode { get; set; }

        [Required]
        private double InitialResPrice { get; set; }

        [Required]
        private string Status { get; set; }

        /*public dynamic GetReservationItem(string command)
        {
            switch (command)
            {
                case "ResId":
                    return ReservationId;
                case "GuestId":
                    return ReserveGuestId.ToString();
                case "NoOfGuest":
                    return NumOfGuest.ToString();
                case "RoomType":
                    return RoomType;
                case "Start":
                    return StartDate.ToString("dd/MM/yyyy HH:mm:ss");
                case "End":
                    return EndDate.ToString("dd/MM/yyyy HH:mm:ss");
                case "Remark":
                    return Remark;
                case "Mod":
                    return LastModified.ToString("dd/MM/yyyy HH:mm:ss");
                case "Promo":
                    return PromoCode;
                case "Price":
                    return InitialResPrice.ToString("g2");
                case "Status":
                    return Status;
                default:
                    return "No such attribute exist!!!";
            }
        }*/

        public bool SetReservationItem(string command, dynamic value)
        {
            switch (command)
            {
                case "GuestId": 
                    ReserveGuestId = value;
                    return true;
                case "NoOfGuest":
                    NumOfGuest = value;
                    return true;
                case "RoomType":
                    RoomType = value;
                    return true;
                case "Start":
                    StartDate = value;
                    return true;
                case "End":
                    EndDate = value;
                    return true;
                case "Remark":
                    Remark = value;
                    return true;
                case "Mod":
                    LastModified = value;
                    return true;
                case "Promo":
                    PromoCode = value;
                    return true;
                case "Price":
                    InitialResPrice = value;
                    return true;
                case "Status":
                    Status = value;
                    return true;
                default:
                    return false;
            }
        }
    }
}
