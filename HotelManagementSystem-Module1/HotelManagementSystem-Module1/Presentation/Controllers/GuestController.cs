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

        [HttpGet("GetByName/{name}")]
        public ActionResult<IEnumerable<GuestViewModel>> GetByName([FromRoute]string name)
        {
            IEnumerable<Guest> guests = _guestService.SearchByGuestName(name);
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        [HttpGet("GetByPassportNumber/{passportNumber}")]
        public ActionResult<IEnumerable<GuestViewModel>> GetByPassPortNumber([FromRoute]string passportNumber)
        {
            IEnumerable<Guest> guests = _guestService.SearchByGuestPassportNumber(passportNumber);
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<GuestViewModel>> GetAll()
        {
            IEnumerable<Guest> guests = _guestService.RetrieveGuests();
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        [HttpPost("NewGuest")]
        public IActionResult Create([FromBody]string firstName, [FromBody]string lastName, [FromBody]string guestType, [FromBody]string email, [FromBody]string passportNumber)
        {
            if(_guestService.RegisterGuest(new Guest(firstName, lastName, guestType, email, passportNumber)))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("UpdateGuest")]
        public IActionResult Update([FromBody]int guestId, [FromBody]string firstName = null, [FromBody]string lastName = null, [FromBody]string guestType = null, [FromBody]string email = null, [FromBody]string passportNumber = null)
        {
            Guest guest = _guestService.SearchByGuestId(guestId);
            if (guest != null)
            {
                guest.UpdateGuestDetails(firstName, lastName, email, guestType, passportNumber);
                if (_guestService.UpdateGuest(guest))
                    return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("DeleteGuest/{guestId}")]
        public IActionResult Delete([FromRoute]int guestId)
        {
            if (_guestService.DeleteGuest(guestId))
                return Ok();
            else
                return BadRequest();
        }        
    }
}
