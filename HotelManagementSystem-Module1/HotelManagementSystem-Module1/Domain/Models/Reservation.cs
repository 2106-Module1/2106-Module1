using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

/*
 * Owner of Model Class: Mod 1 Team 4
 */
namespace HotelManagementSystem_Module1.Domain.Models
{
    public class Reservation
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

        private double InitialResPrice { get; set; }

        [Required]
        private string Status { get; set; }

        public Reservation()
        {

        }

        private Reservation(Dictionary<string, object> reservationDictionary)
        {
            ReserveGuestId = (int)reservationDictionary["guestID"];
            NumOfGuest = (int)reservationDictionary["numOfGuest"];
            RoomType = (string)reservationDictionary["roomType"];
            StartDate = (DateTime)reservationDictionary["start"];
            EndDate = (DateTime)reservationDictionary["end"];
            Remark = (string)reservationDictionary["remark"];
            LastModified = (DateTime)reservationDictionary["modified"];
            PromoCode = (string)reservationDictionary["promoCode"];
            InitialResPrice = (double)reservationDictionary["price"];
            Status = (string)reservationDictionary["status"];
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

        public object SetReservation(Dictionary<string, object> resDetail)
        {
            Reservation obj = new Reservation(resDetail);

            return obj;
        }

        /**
         * Allow updating of object when required.
         * example: obj.UpdateReservation(newNumOfGuest: 3);
         */
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
