﻿using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.Domain
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IFacilityReservationRepository _facilityReservationRepository;

        public GuestService(IGuestRepository guestRepository, IFacilityReservationRepository facilityReservationRepository)
        {
            _guestRepository = guestRepository;
            _facilityReservationRepository = facilityReservationRepository;
        }

        public bool DeleteGuest(Guest guest)
        {
            if (guest == null || _guestRepository.GetById(guest.GuestIdDetails()) == null || guest.OutstandingChargesDetails() > 0)
                return false;

            // Delete all facility reservations made by guest
            IList<FacilityReservation> facilityReservations = _facilityReservationRepository.GetByReserveeId(guest.GuestIdDetails()).ToList();
            foreach(var facilityReservation in facilityReservations)
            {
                _facilityReservationRepository.Delete(facilityReservation);
            }

            _guestRepository.Delete(guest);
            return true;
        }

        public bool DeleteGuest(int guestId)
        {
            Guest guest = _guestRepository.GetById(guestId);
            if (guest == null || guest.OutstandingChargesDetails() > 0)
                return false;

            // Delete all facility reservations made by guest
            IList<FacilityReservation> facilityReservations = _facilityReservationRepository.GetByReserveeId(guest.GuestIdDetails()).ToList();
            foreach (var facilityReservation in facilityReservations)
            {
                _facilityReservationRepository.Delete(facilityReservation);
            }

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

        public bool UpdateGuest(Guest guest)
        {
            bool checkEmail = false;
            if (!guest.EmailDetails().Equals("") && !guest.FirstNameDetails().Equals("") && !guest.LastNameDetails().Equals("")&& !guest.PassportNumberDetails().Equals(""))
            {
                try
                {
                    bool emailValid = Regex.IsMatch(guest.EmailDetails(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    if (emailValid)
                    {
                        checkEmail = true;
                        
                    }
                    else
                    {
                        checkEmail =  false;
                    }
                }
                catch (RegexMatchTimeoutException)
                {

                }
            }
            if (checkEmail)
            {
                _guestRepository.Update(guest);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
