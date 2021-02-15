﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace HotelManagementSystem_Module1.Domain.Models
{
    public class Reservation
    {
        
        [Key] 
        private static int ReservationId { get; set; }

        [Required(ErrorMessage = "Reservee Guest ID is required.")]
        private static int ReserveGuestId { get; set; }

        [Required(ErrorMessage = "Number of Guest is required.")]
        private static int NumOfGuest { get; set; }

        [Required(ErrorMessage = "Room Type is required.")]
        private static string RoomType { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        private static DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        private static DateTime EndTime { get; set; }

        private static string Remark { get; set; }

        private static DateTime LastModified { get; set; }

        private static string PromoCode { get; set; }

        private static double InitialResPrice { get; set; }

        private static string Status { get; set; }
        
        private Reservation(Dictionary<string, object> reservationDictionary)
        {
            NumOfGuest = (int)reservationDictionary["numOfGuest"];
            RoomType = (string) reservationDictionary["roomType"];
            StartTime = (DateTime) reservationDictionary["start"];
            EndTime = (DateTime)reservationDictionary["end"];
            Remark = (string) reservationDictionary["remark"];
            LastModified = (DateTime) reservationDictionary["modified"];
            PromoCode = (string) reservationDictionary["promoCode"];
            InitialResPrice = (double) reservationDictionary["price"];
            Status = (string) reservationDictionary["status"];
        }

        public Reservation()
        {

        }

        private Dictionary<string, object> ReservationDetail()
        {
            var reservationDetail = new Dictionary<string, object>();

            reservationDetail["resID"] = ReservationId;
            reservationDetail["guestID"] = ReserveGuestId;
            reservationDetail["numOfGuest"] = NumOfGuest;
            reservationDetail["roomType"] = RoomType;
            reservationDetail["start"] = StartTime;
            reservationDetail["end"] = EndTime;
            reservationDetail["remark"] = Remark;
            reservationDetail["modified"] = LastModified;
            reservationDetail["promoCode"] = PromoCode;
            reservationDetail["price"] = InitialResPrice;
            reservationDetail["status"] = Status;

            return reservationDetail;
        }

        private static void UpdateReservation(Dictionary<string, object> updateDictionary)
        {
            
        }

        public Dictionary<string, object> GetReservation()
        {
            Dictionary<string, object> reservationDetail = ReservationDetail();

            return reservationDetail;
        }

        public object CreateReservation(Dictionary<string, object> resDetail)
        {
            Reservation obj = new Reservation(resDetail);

            return obj;
        }

        public static void Update(Dictionary<string, object> updateDictionary)
        {
            
        }
    }
}
