using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Models
{
    public class GuestViewModel
    {
        private int guestId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters")] 
        private String firstName { get; set; }

        private String lastName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]

        private String guestType { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]

        private String email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
        private String passportNumber { get; set; }

        public GuestViewModel(string firstName, string lastName, string guestType, string email, string passportNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.guestType = guestType;
            this.email = email;
            this.passportNumber = passportNumber;
        }

        public int getGuestId()
        {
            return this.guestId;
        }

        public String getFullName()
        {
            return this.firstName + " " + this.lastName;
        }

        public string getGuestType()
        {
            return this.guestType;
        }

        public String getEmail()
        {
            return this.email;
        }

        public String getPassportNumber()
        {
            return this.passportNumber;
        }

    }
}
