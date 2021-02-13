using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;

        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public void DeleteGuest(Guest guest)
        {
            _guestRepository.Delete(guest);
        }

        public void DeleteGuest(int guestId)
        {
            _guestRepository.Delete(_guestRepository.GetById(guestId));
        }

        public void RegisterGuest(Guest guest)
        {
            _guestRepository.Insert(guest);
        }

        public IEnumerable<Guest> RetrieveGuests()
        {
            return _guestRepository.GetAll();
        }

        public IEnumerable<Guest> SearchByGuestName(string name)
        {
            return _guestRepository.GetByName(name);
        }

        public IEnumerable<Guest> SearchByGuestPassportNumber(string passportNumber)
        {
            return _guestRepository.GetByPassportNumber(passportNumber);
        }

        public void UpdateGuest(Guest guest)
        {
            _guestRepository.Update(guest);
        }
    }
}
