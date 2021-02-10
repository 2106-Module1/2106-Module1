using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Models
{
    public class Guest
    {
        [Key]
        private int GuestId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
        private string FirstName { get; set; }

        private string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
        private string GuestType { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        private string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(100, ErrorMessage = "{0} cannot be more than {1} characters")]
        private string PassportNumber { get; set; }

        public Guest()
        {
        }

        public Guest(string firstName, string lastName, string guestType, string email, string passportNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            GuestType = guestType;
            Email = email;
            PassportNumber = passportNumber;
        }

        public int GuestIdDetails()
        {
            return GuestId;
        }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }

        public string GuestTypeDetails()
        {
            return GuestType;
        }

        public string EmailDetails()
        {
            return Email;
        }

        public string PassportNumberDetails()
        {
            return PassportNumber;
        }

        public void UpdateGuestDetails(string newFirstName = null, string newLastName = null, string newEmail = null, string newGuestType = null, string newPassportNumber = null)
        {
            FirstName = newFirstName ?? FirstName;
            LastName = newLastName ?? LastName;
            Email = newEmail ?? Email;
            GuestType = newGuestType ?? GuestType;
            PassportNumber = newPassportNumber ?? PassportNumber;
        }
    }
}
