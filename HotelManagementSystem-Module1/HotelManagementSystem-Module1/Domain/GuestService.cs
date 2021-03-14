﻿using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
            // Server side form validation
            bool formValuesIsValid = false;
            bool isNotDuplicateGuest = false;

            if (!guest.FirstNameDetails().Equals("") && !guest.LastNameDetails().Equals("") && !guest.GuestIdDetails().Equals("") && !guest.EmailDetails().Equals("") && (!guest.PassportNumberDetails().Equals("") && guest.PassportNumberDetails().Length >=9)) {
                try
                {
                    bool emailFormatIsValid = Regex.IsMatch(guest.EmailDetails(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    if (emailFormatIsValid) {
                        formValuesIsValid = true;
                    }
                }
                catch (RegexMatchTimeoutException) { 
                
                }            
            }

            // Check if duplicate guest
            IEnumerable<Guest> guests = RetrieveGuests();
            foreach (Guest g in guests) {
                if (!g.PassportNumberDetails().Equals(guest.PassportNumberDetails()) && !g.EmailDetails().Equals(guest.EmailDetails())) {
                    isNotDuplicateGuest = true;
                }
            }


            if (formValuesIsValid && isNotDuplicateGuest) {
                _guestRepository.Insert(guest);
                return true;
            }
            else
            {
                return false;
            }
            
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
        public bool SearchOutstandingCharges(int outstandingCharges)
        {
            if (_guestRepository.GetOutstandingCharges(outstandingCharges) == null)
            {
                return true;
                
            }
            else
            {
                return false;
            }
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
