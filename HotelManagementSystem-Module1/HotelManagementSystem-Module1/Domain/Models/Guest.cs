using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
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

        private int OutstandingCharges { get; set; }

        private Guest()
        {
        }

        public Guest(int guestId, string firstName, string lastName, string guestType, string email, string passportNumber)
        {
            GuestId = guestId;
            FirstName = firstName;
            LastName = lastName;
            GuestType = guestType;
            Email = email;
            PassportNumber = passportNumber;
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

        public string FirstNameDetails()
        {
            return FirstName;
        }

        public string LastNameDetails()
        {
            return LastName;
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

        public int OutstandingChargesDetails()
        {
            return OutstandingCharges;
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
