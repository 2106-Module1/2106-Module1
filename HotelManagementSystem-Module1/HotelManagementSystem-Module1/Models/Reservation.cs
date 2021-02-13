using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace HotelManagementSystem_Module1.Models
{
    public class Reservation
    {
        [Key] 
        private int reservationID { get; set; }

        [Required(ErrorMessage = "Reservee Guest ID is required.")]
        private int reserveeGuestId { get; set; }

        [Required(ErrorMessage = "Number of Guest is required.")]
        private int numOfGuest { get; set; }

        [Required(ErrorMessage = "Room Type is required.")]
        private string roomType { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        private DateTime startTime { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        private DateTime endTime { get; set; }

        private string remark { get; set; }

        private DateTime lastModified { get; set; }

        private string promoCode { get; set; }

        private double initialResPrice { get; set; }

        private string status { get; set; }
        
        private Reservation(Dictionary<string, object> reservationDictionary)
        {
            reserveeGuestId = (int)reservationDictionary["guestID"];
            numOfGuest = (int) reservationDictionary["numOfGuest"];
            roomType = (string) reservationDictionary["roomType"];
            startTime = (DateTime) reservationDictionary["start"];
            endTime = (DateTime)reservationDictionary["start"];
            remark = (string) reservationDictionary["remark"];
            lastModified = (DateTime) reservationDictionary["modified"];
            promoCode = (string) reservationDictionary["promoCode"];
            initialResPrice = (double) reservationDictionary["price"];
            status = (string) reservationDictionary["status"];
        }

        private object ReservationDetail()
        {
            var reservationDetail = new Dictionary<string, object>();

            reservationDetail["resID"] = reservationID;
            reservationDetail["guestID"] = reserveeGuestId;
            reservationDetail["numOfGuest"] = numOfGuest;
            reservationDetail["roomType"] = roomType;
            reservationDetail["start"] = startTime;
            reservationDetail["start"] = endTime;
            reservationDetail["remark"] = remark;
            reservationDetail["modified"] = lastModified;
            reservationDetail["promoCode"] = promoCode;
            reservationDetail["price"] = initialResPrice;
            reservationDetail["status"] = status;

            return reservationDetail;
        }

        public object GetReservation()
        {
            return ReservationDetail();
        }

        public void CreateReservation(Dictionary<string, object> resDetail)
        {
            _ = new Reservation(resDetail);
        }
    }
}
