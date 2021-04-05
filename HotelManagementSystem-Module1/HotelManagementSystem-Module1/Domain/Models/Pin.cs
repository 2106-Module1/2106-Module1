using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class Pin
    {
        [Key]
        private int PinID { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
        private string PinNumber { get; set; }

        public Pin(int PinID, string PinNumber)
        {
            this.PinID = PinID;
            this.PinNumber = PinNumber;
        }

        public int PinIdDetails()
        {
            return PinID;
        }

        public string PinNumberDetails()
        {
            return PinNumber;
        }

        public void UpdatePin(string newPinNumber)
        {
            PinNumber = newPinNumber ?? PinNumber;
        }
    }
}
