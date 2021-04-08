using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/*
 * Owner of Model Class: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    public class Reservation
    {

        [Key]
        private int ReservationId { get; set; }

        [Required(ErrorMessage = "Reserve Guest ID is required.")]
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

        public Reservation() { }

        private bool CreateReservationItem(string command, dynamic value)
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

        private Dictionary<string, object> ReservationDetail()
        {
            var reservationDetail = new Dictionary<string, object>
            {
                ["resID"] = ReservationId,
                ["guestID"] = ReserveGuestId,
                ["numOfGuest"] = NumOfGuest,
                ["roomType"] = RoomType,
                ["start"] = StartDate,
                ["end"] = EndDate,
                ["remark"] = Remark,
                ["modified"] = LastModified,
                ["promoCode"] = PromoCode,
                ["price"] = InitialResPrice,
                ["status"] = Status
            };
            return reservationDetail;
        }

        public Dictionary<string, object> GetReservation()
        {
            Dictionary<string, object> reservationDetail = ReservationDetail();
            return reservationDetail;
        }

        public bool SetReservationItem(string command, dynamic value)
        {
            return CreateReservationItem(command, value);
        }
        
        public void UpdateReservation(int? newNumOfGuest = null, string newRoomType = null, DateTime? newStartDate = null, DateTime? newEndDate = null,
            string newRemark = null, DateTime? newLastModified = null, string newPromoCode = null, double? newInitialResPrice = null, string newStatus = null)
        {
            NumOfGuest = newNumOfGuest ?? NumOfGuest;
            RoomType = newRoomType ?? RoomType;
            StartDate = newStartDate ?? StartDate;
            EndDate = newEndDate ?? EndDate;
            Remark = newRemark ?? Remark;
            LastModified = newLastModified ?? LastModified;
            PromoCode = newPromoCode ?? PromoCode;
            InitialResPrice = newInitialResPrice ?? InitialResPrice;
            Status = newStatus ?? Status;
        }
    }
}
