using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Models
{
    public class FacilityReservation
    {
        [Key]
        private int FacilityReservationId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        private int ReserveeGuestId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        private int FacilityId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        private int Pax { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        private DateTime StartTime { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        private DateTime EndTime { get; set; }

        private bool Cancelled { get; set; } = false;

        public FacilityReservation()
        {
        }

        public FacilityReservation(int reserveeGuestId, int facilityId, int pax, DateTime startTime, DateTime endTime)
        {
            ReserveeGuestId = reserveeGuestId;
            FacilityId = facilityId;
            Pax = pax;
            StartTime = startTime;
            EndTime = endTime;
            Cancelled = false;
        }

        public int ReservationIdDetails()
        {
            return FacilityReservationId;
        }

        public int ReserveeIdDetails()
        {
            return ReserveeGuestId;
        }

        public int FacilityIdDetails()
        {
            return FacilityId;
        }

        public int NumberOfPax()
        {
            return Pax;
        }

        public DateTime StartTimeDetails()
        {
            return StartTime;
        }

        public DateTime EndTimeDetails()
        {
            return EndTime;
        }

        public bool IsCancelled()
        {
            return Cancelled;
        }

        public void CancelReservation()
        {
            Cancelled = true;
        }

        public void UpdateReservation(int? newPax = null, DateTime? newStartTime = null, DateTime? newEndTime = null)
        {
            Pax = newPax ?? Pax;
            StartTime = newStartTime ?? StartTime;
            EndTime = newEndTime ?? EndTime;
        }
    }
}
