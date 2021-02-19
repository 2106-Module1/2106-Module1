using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Presentation.ViewModels
{
    public class GuestViewModel
    {
        private int GuestId { get; }
        private string FirstName { get; }
        private string LastName { get; }
        private string GuestType { get; }
        private string Email { get; }
        private string PassportNumber { get; }
        private int OutstandingCharges { get; set; }

        public GuestViewModel(int guestId, string firstName, string lastName, string guestType, string email, string passportNumber)
        {
            GuestId = guestId;
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
    }
}
