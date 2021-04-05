using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class GuestFactory
    {
        public GuestFactory() { }
        public static Guest CreateGuest(string firstName, string lastName, string guestType, string email, string passportNumber)
        {
            if(guestType == "VIP")
            {
                return new VIP(firstName,lastName, guestType, email, passportNumber);
            }
            else if (guestType == "Corporate")
            {
                return new Corporate(firstName, lastName, guestType, email, passportNumber);
            }
            else if (guestType == "Regular")
            {
                return new Regular(firstName, lastName, guestType, email, passportNumber);
            }
            else
            {
                return new Guest(firstName, lastName, guestType, email, passportNumber);
            }
        }
    }
}
