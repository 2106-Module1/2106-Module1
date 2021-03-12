using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class Regular : Guest
    {
        public Regular(string firstName, string lastName, string guestType, string email, string passportNumber): base(firstName, lastName, guestType, email, passportNumber)
        {

        }
    }
}
