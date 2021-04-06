using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HotelManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.Models;
using HotelManagementSystem.DataSource;
using HotelManagementSystem.Presentation.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;
        private readonly IAuthenticate _authenticator;

        public GuestController(IGuestService guestService, IAuthenticate authenticator)
        {
            _guestService = guestService;
            _authenticator = authenticator;
        }

        public ActionResult Index([FromQuery] string? selectGuest)
        {

            IEnumerable<GuestViewModel> GuestList = GetAll();
            if (selectGuest != null)
            {
                if (selectGuest.Equals("yes"))
                {
                    ViewBag.create = true;
                }
            }
            else {
                ViewBag.create = false;
            
            }
            return View(GuestList);
        }

        [HttpGet]
        public ActionResult CreateGuest()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UpdateGuest(int guestID)
        {
            Guest guest = _guestService.SearchByGuestId(guestID);
            GuestViewModel guestViewModel = (new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            ViewBag.guest = guestViewModel;
            return View();
                
        }
        [HttpPost]
        public ActionResult UpdateGuest(IFormCollection form)
        {
            string guestID = form["guestID"];
            int guestid = Convert.ToInt32(guestID);
            string firstName = form["FirstName"];
            string lastName = form["LastName"];
            string guestType = form["GuestType"];
            string email = form["Email"];
            string passportNumber = form["PassportNumber"];
            string secretPin = form["secretpin"];

            if (_authenticator.AuthenticatePin(secretPin) && Update(guestid,firstName, lastName, guestType, email, passportNumber))
            {
                TempData["UpdateGuestMessage"] = "Success";
                return Redirect("/Guest");
            }
            else
            {
                ViewData["UpdateGuestMessage"] = "Error";
                Guest guest = _guestService.SearchByGuestId(guestid);
                GuestViewModel guestViewModel = (new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
                ViewBag.guest = guestViewModel;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreateGuest(IFormCollection form)
        {
            string firstName = form["FirstName"];
            string lastName = form["LastName"];
            string guestType = form["GuestType"];
            string email = form["Email"];
            string passportNumber = form["PassportNumber"];
            if (Create(firstName, lastName, guestType, email, passportNumber))
            {
                IEnumerable<GuestViewModel> guest = GetByPassPortNumber(passportNumber);
                return RedirectToAction("CreateReservation","ReservationCreation",new {GuestId = guest.FirstOrDefault().GuestIdDetails() });
            }
            else {
                ViewData["CreateGuestMessage"] = "Error";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteGuest(string guestID,string secretPin)
        {
            //This will need to check if there exists outstanding charges.

            int guestid = Convert.ToInt32(guestID);
            Guest guest = _guestService.SearchByGuestId(guestid);
            if (guest.OutstandingChargesDetails()==0)
            {
                // This will check for secret pin validity
                if (!_authenticator.AuthenticatePin(secretPin)) {
                    TempData["DeleteGuestMessage"] = "InvalidPin";
                    return RedirectToAction("Index", "Guest");
                }
                _guestService.DeleteGuest(guestid);
                TempData["DeleteGuestMessage"] = "NoCharges";
                return RedirectToAction("Index", "Guest");
            }
            else
            {
                TempData["DeleteGuestMessage"] = "HasCharges";
                return RedirectToAction("Index", "Guest");
            }
                
        }

        /// <summary>
        /// Retrieves all guests with a matching name
        /// </summary>
        /// <param name="name"> Name to match                                           </param>
        /// <returns>           List of guest information represented in a viewmodel    </returns>
        [NonAction]
        public IEnumerable<GuestViewModel> GetByName(string name)
        {
            IEnumerable<Guest> guests = _guestService.SearchByGuestName(name);
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        /// <summary>
        /// Retrieves all guests with a matching passport nuber
        /// </summary>
        /// <param name="passportNumber">   Passport number to match                                </param>
        /// <returns>                       List of guest information represented in a viewmodel    </returns>
        [NonAction]
        public IEnumerable<GuestViewModel> GetByPassPortNumber(string passportNumber)
        {
            IEnumerable<Guest> guests = _guestService.SearchByGuestPassportNumber(passportNumber);
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        /// <summary>
        /// Retrieves all the guest information stored
        /// </summary>
        /// <returns>List of guest information represented in a viewmodel</returns>
        [NonAction]
        public IEnumerable<GuestViewModel> GetAll()
        {
            IEnumerable<Guest> guests = _guestService.RetrieveGuests();
            List<GuestViewModel> guestResults = new List<GuestViewModel>();
            foreach (var guest in guests)
                guestResults.Add(new GuestViewModel(guest.GuestIdDetails(), guest.FirstNameDetails(), guest.LastNameDetails(), guest.GuestTypeDetails(), guest.EmailDetails(), guest.PassportNumberDetails()));
            return guestResults;
        }

        /// <summary>
        /// Create a new guest
        /// </summary>
        /// <param name="firstName">        The firstname of the guest                          </param>
        /// <param name="lastName">         The lastname of the guest                           </param>
        /// <param name="guestType">        The guest type of the guest                         </param>
        /// <param name="email">            The email of the guest                              </param>
        /// <param name="passportNumber">   The passport number of the guest                    </param>
        /// <returns>                       Result of whether the guest creation was successful </returns>
        [NonAction]
        public bool Create(string firstName, string lastName, string guestType, string email, string passportNumber)
        {
            if(_guestService.RegisterGuest(GuestFactory.CreateGuest(firstName, lastName, guestType, email, passportNumber)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Update a guest's information
        /// </summary>
        /// <param name="guestId">          Guest id of the guest to update             </param>
        /// <param name="firstName">        New firstname value                         </param>
        /// <param name="lastName">         New lastname value                          </param>
        /// <param name="guestType">        New guest type value                        </param>
        /// <param name="email">            New email value                             </param>
        /// <param name="passportNumber">   New passport number value                   </param>
        /// <returns>                       Result of whether the update was successful </returns>
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

        /// <summary>
        /// Delete a guest by guestId
        /// </summary>
        /// <param name="guestId">  The id of the guest to delete                   </param>
        /// <returns>               Result of whether the deletion was successful   </returns>
        [NonAction]
        public bool Delete(int guestId)
        {
            return _guestService.DeleteGuest(guestId);
        }        
    }
}
