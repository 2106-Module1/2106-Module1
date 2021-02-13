using System;
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

namespace HotelManagementSystem_Module1.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;
        private readonly Guest _guest;

        public GuestController(IGuestService guestRepository, Guest guest)
        {
            _guestService = guestRepository;
            _guest = guest;
        }

        public ActionResult Index()
        {
            // This will return back to the view 
            // Maye require to changes once view layout/design is out
            return View();
        }

        public void getByName(String Name)
        {
            _guestService.SearchByGuestName(Name);
        }

        public void getByPassPortNumber(String PassportNumber)
        {
            _guestService.SearchByGuestPassportNumber(PassportNumber);
        }

        public void getAll()
        {
            _guestService.RetrieveGuests();
        }

        public void Create()
        {
            _guestService.RegisterGuest(_guest);
        }

        public void Update()
        {
            _guestService.UpdateGuest(_guest);
        }

        public void Delete()
        {
            _guestService.DeleteGuest(_guest);
        }        
    }
}
