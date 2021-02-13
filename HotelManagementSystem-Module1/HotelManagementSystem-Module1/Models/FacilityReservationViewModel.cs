using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Models
{
    public class FacilityReservationViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        private int facilityReservationId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        private int reserveeGuestId { get;set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Range(1, 100, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        private int pax { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        private DateTime startTime { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        private DateTime endTime { get; set; }

        private bool cancelled { get; set; }

        public int getFacilityReservationId()
        {
            return this.facilityReservationId;
        }

        public int getReserveedId()
        {
            return this.reserveeGuestId;
        }

        public int getPax()
        {
            return this.pax;
        }

        public DateTime getStartTime()
        {
            return this.startTime;
        }

        public DateTime GetDateTime()
        {
            return this.endTime;
        }

        public bool isCancelled()
        {
            return this.cancelled;
        }
    }
}
