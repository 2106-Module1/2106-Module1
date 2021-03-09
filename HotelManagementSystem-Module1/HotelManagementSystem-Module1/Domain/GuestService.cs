using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;

        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public bool DeleteGuest(Guest guest)
        {
            if (_guestRepository.GetById(guest.GuestIdDetails()) == null)
                return false;
            _guestRepository.Delete(guest);
            return true;
        }

        public bool DeleteGuest(int guestId)
        {
            Guest guest = _guestRepository.GetById(guestId);
            if (guest == null)
                return false;
            _guestRepository.Delete(guest);
            return true;
        }

        public bool RegisterGuest(Guest guest)
        {
            _guestRepository.Insert(guest);
            return true;
        }

        public IEnumerable<Guest> RetrieveGuests()
        {
            return _guestRepository.GetAll();
        }

        public Guest SearchByGuestId(int guestId)
        {
            return _guestRepository.GetById(guestId);
        }

        public IEnumerable<Guest> SearchByGuestName(string name)
        {
            return _guestRepository.GetByName(name);
        }

        public IEnumerable<Guest> SearchByGuestPassportNumber(string passportNumber)
        {
            return _guestRepository.GetByPassportNumber(passportNumber);
        }

        public bool UpdateGuest(Guest guest)
        {
            if (_guestRepository.GetById(guest.GuestIdDetails()) == null)
                return false;
            _guestRepository.Update(guest);
            return true;
        }
    }
}
