using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Presentation.ViewModels
{
    public class FacilityReservationViewModel
    {
        private int FacilityReservationId { get; }
        private int ReserveeGuestId { get; }
        private int FacilityId { get; }
        private int Pax { get; }
        private DateTime StartTime { get; }
        private DateTime EndTime { get; }
        private bool Cancelled { get; }

        public FacilityReservationViewModel(int facilityReservationId, int reserveeGuestId, int facilityId, int pax, DateTime startTime, DateTime endTime)
        {
            FacilityReservationId = facilityReservationId;
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
    }
}
