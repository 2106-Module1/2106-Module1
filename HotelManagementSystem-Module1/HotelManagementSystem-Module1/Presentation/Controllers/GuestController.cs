﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.DataSource;
using HotelManagementSystem_Module1.Presentation.ViewModels;

namespace HotelManagementSystem_Module1.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestRepository)
        {
            _guestService = guestRepository;
        }

        public ActionResult Index()
        {
            // This will return back to the view 
            // Maye require to changes once view layout/design is out
            return View();
        }

        [NonAction]
        public IEnumerable<GuestViewModel> GetByName(string name)
        {
            IEnumerable<Guest> guests = _guestService.SearchByGuestName(name);
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        [NonAction]
        public IEnumerable<GuestViewModel> GetByPassPortNumber(string passportNumber)
        {
            IEnumerable<Guest> guests = _guestService.SearchByGuestPassportNumber(passportNumber);
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        [NonAction]
        public IEnumerable<GuestViewModel> GetAll()
        {
            IEnumerable<Guest> guests = _guestService.RetrieveGuests();
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        [NonAction]
        public bool Create(string firstName, string lastName, string guestType, string email, string passportNumber)
        {
            if(_guestService.RegisterGuest(new Guest(firstName, lastName, guestType, email, passportNumber)))
                return true;
            else
                return false;
        }

        [NonAction]
        public bool Update(int guestId, string firstName = null, string lastName = null, string guestType = null, string email = null, string passportNumber = null)
        {
            Guest guest = _guestService.SearchByGuestId(guestId);
            if (guest != null)
            {
                guest.UpdateGuestDetails(firstName, lastName, email, guestType, passportNumber);
                if (_guestService.UpdateGuest(guest))
                    return true;
            }

            return false;
        }

        [NonAction]
        public bool Delete(int guestId)
        {
            if (_guestService.DeleteGuest(guestId))
                return true;
            else
                return false;
        }        
    }
}