using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.Presentation.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Dynamic;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.Controllers
{
    public class FacilityReservationController : Controller
    {
        private readonly IFacilityReservationService _facilityReservationService;
        private readonly IPublicArea _publicArea;
        private readonly IGuestService _guestService;
        private readonly IAuthenticate _authenticator;

        public FacilityReservationController(IFacilityReservationService facilityReservationService, IPublicArea publicArea,
            IGuestService guestService, IAuthenticate authenticator)
        {
            _facilityReservationService = facilityReservationService;
            _publicArea = publicArea;
            _guestService = guestService;
            _authenticator = authenticator;
        }

        public ActionResult Index()
        {
            // Retrieve all facility based on Mod 3 Team 06 function
            List<PublicAreaDTO> fullfacilityList = _publicArea.getAllFacilityResults();

            // For loop to store existing facility to populate View Form DropDownList
            Dictionary<int, string> namefacilityList = new Dictionary<int, string>();
            if (fullfacilityList.Count > 0)
            {
                foreach (PublicAreaDTO fac in fullfacilityList)
                {
                    namefacilityList.Add(fac.public_area_id, fac.public_area_name);
                }
            }

            // Retrieve all existing guests
            IEnumerable<Guest> guestList = _guestService.RetrieveGuests();

            // For loop to store existing guest to populate View Form DropDownList
            Dictionary<int, string> existguestList = new Dictionary<int, string>();
            foreach (var guest in guestList)
            {
                existguestList.Add(guest.GuestIdDetails(), guest.FirstNameDetails() + " " + guest.LastNameDetails());
            }

            // Passing data over to View Page via ViewBag
            ViewBag.namefacilityList = namefacilityList;
            ViewBag.existguestList = existguestList;

            IEnumerable<FacilityReservationViewModel> facilityReservationList = GetAll();
            return View(facilityReservationList);
        }

        [HttpGet]
        public ActionResult CreateFacilityReservation()
        {
            // This is to set the timing
            Dictionary<string, string> facilityDateTime = new Dictionary<string, string>();
            string curDT = DateTime.Now.ToString("yyyy-MM-dd");
            string oneltrDT = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm");

            facilityDateTime.Add("StartTime", curDT);
            facilityDateTime.Add("MinTime", curDT);
            facilityDateTime.Add("EndTime", oneltrDT);

            // Retrieve all facility based on Mod 3 Team 06 function
            List<PublicAreaDTO> fullfacilityList = _publicArea.getAllFacilityResults();

            // For loop to store existing facility to populate View Form DropDownList
            Dictionary<int, string> namefacilityList = new Dictionary<int, string>();
            if (fullfacilityList.Count > 0)
            {
                foreach (PublicAreaDTO fac in fullfacilityList)
                {
                    namefacilityList.Add(fac.public_area_id, fac.public_area_name);
                }
            }

            // Retrieve all existing guests
            IEnumerable<Guest> guestList = _guestService.RetrieveGuests();

            // For loop to store existing guest to populate View Form DropDownList
            Dictionary<int, string> existguestList = new Dictionary<int, string>();
            foreach (var guest in guestList)
            {
                existguestList.Add(guest.GuestIdDetails(), guest.FirstNameDetails() + " " + guest.LastNameDetails());
            }

            // Passing data over to View Page via ViewBag
            ViewBag.facilityTemp = facilityDateTime;
            ViewBag.namefacilityList = namefacilityList;
            ViewBag.existguestList = existguestList;

            return View();
        }

        [HttpPost]
        public ActionResult CreateFacilityReservation(IFormCollection form)
        {
            // This is to retrieve the data from FORM
            int guestId = int.Parse(form["guestid"]);
            int facilityId = int.Parse(form["facilityType"]);
            int facilityPax = int.Parse(form["pax"]);
            string hourSelect = form["hourSelected"];
            //[0]: hour, [1]: number of pax
            string[] hourandpax = hourSelect.Split(",");
            DateTime startTime = Convert.ToDateTime(form["startTime"]);
            int hourSelected = int.Parse(hourandpax[0]);
            int year = startTime.Year;
            int month = startTime.Month;
            int day = startTime.Day;
            int hour = hourSelected;
            int minutes = 00;
            String format = "AM";
            DateTime Rdatetime = new DateTime(year, month, day,
                                             (format.ToUpperInvariant() == "PM" && hour < 12) ?
                                                 hour + 12 : hour,
                                             minutes,
                                             00);
            DateTime endTime = Convert.ToDateTime(form["endTime"]);


            if (facilityPax <= int.Parse(hourandpax[1]))
            {
                // This is success scenario
                if (Create(guestId, facilityId, facilityPax, Rdatetime, endTime))
                {
                    TempData["Message"] = "Created";
                    // This required to change to facilityReservation landing page.
                    return RedirectToAction("Index", "FacilityReservation");
                }
                else
                {
                    // This is unsucess scenario
                }
            }
            else
            {
                TempData["Unsuccessful"] = "Create Unsuccessful due to over number of pax, Please try it again";
                return RedirectToAction("CreateFacilityReservation", "FacilityReservation");
            }

            return View();
        }

        [HttpGet]
        public ActionResult UpdateFacilityReservation(string selectedFacResId, string selectedGusResID)
        {
            // Retrieve all facility based on Mod 3 Team 06 function
            List<PublicAreaDTO> fullfacilityList = _publicArea.getAllFacilityResults();

            // For loop to store existing facility to populate View Form DropDownList
            Dictionary<int, string> namefacilityList = new Dictionary<int, string>();
            if (fullfacilityList.Count > 0)
            {
                foreach (PublicAreaDTO fac in fullfacilityList)
                {
                    namefacilityList.Add(fac.public_area_id, fac.public_area_name);   
                }
            }

            // This is to filter out unrelated record, only return the selected records
            FacilityReservation selectedID = _facilityReservationService.RetrieveByReservationId(int.Parse(selectedFacResId));
            
            Dictionary<string, string> currentRecord = new Dictionary<string, string>();
            currentRecord.Add("FacilityId", selectedID.FacilityIdDetails().ToString());
            currentRecord.Add("ReservationId", selectedID.ReservationIdDetails().ToString());
            currentRecord.Add("ReserveeId", selectedID.ReserveeIdDetails().ToString());
            currentRecord.Add("StartTime", selectedID.StartTimeDetails().ToString("yyyy-MM-dd"));
            currentRecord.Add("EndTime", selectedID.EndTimeDetails().ToString("yyyy-MM-ddTHH:mm"));
            currentRecord.Add("Pax", selectedID.NumberOfPax().ToString());
            currentRecord.Add("SGID", selectedGusResID);
            currentRecord.Add("SFID", selectedFacResId);
            // Retrieve all existing guests
            Guest guestList = _guestService.SearchByGuestId(int.Parse(selectedGusResID));

            // For loop to store existing guest to populate View Form DropDownList
            Dictionary<int, string> existguestList = new Dictionary<int, string>();
            existguestList.Add(guestList.GuestIdDetails(), guestList.FirstNameDetails() + " " + guestList.LastNameDetails());

            // Passing data over to View Page via ViewBag
            ViewBag.currentRecord = currentRecord;
            ViewBag.existguestList = existguestList;
            ViewBag.namefacilityList = namefacilityList;

            return View();
        }

        [HttpPost]
        public ActionResult UpdateFacilityReservation(IFormCollection form)
        {
            // This is to retrieve the data from FORM
            string decision = form["submit"].ToString();
            int reservationId = int.Parse(form["reservationId"]);
            int facilityPax = int.Parse(form["pax"]);
            DateTime endTime = Convert.ToDateTime(form["endTime"]);
            string hourSelect = form["hourSelected"];
            //[0]: hour, [1]: number of pax
            string[] hourandpax = hourSelect.Split(",");
            DateTime startTime = Convert.ToDateTime(form["startTime"]);
            string secretPin = form["secretpin"];
            int hourSelected = int.Parse(hourandpax[0]);
            int year = startTime.Year;
            int month = startTime.Month;
            int day = startTime.Day;
            int hour = hourSelected;
            int minutes = 00;
            String format = "AM";
            DateTime Rdatetime = new DateTime(year, month, day,
                                             (format.ToUpperInvariant() == "PM" && hour < 12) ?
                                                 hour + 12 : hour,
                                             minutes,
                                             00);


            if (facilityPax <= int.Parse(hourandpax[1]))
            {
                // This is to update reservation
                // This is success scenario
                if (_authenticator.AuthenticatePin(secretPin) && Update(reservationId, Rdatetime, endTime, facilityPax))
                {
                    TempData["Message"] = "Updated";
                    // This required to change to facilityReservation landing page.
                    return RedirectToAction("Index", "FacilityReservation");
                }
                else
                {
                    TempData["Message"] = "Update Unsuccessful";
                    return RedirectToAction("UpdateFacilityReservation", "FacilityReservation", new
                    {
                        selectedFacResId = form["selectedFacResId"],
                        selectedGusResID = form["selectedGusResID"]
                    }, null);
                }
            }
            else
            {
                TempData["Message"] = "Update Unsuccessful due to over number of pax";
                return RedirectToAction("UpdateFacilityReservation", "FacilityReservation", new
                {
                    selectedFacResId = form["selectedFacResId"],
                    selectedGusResID = form["selectedGusResID"]
                }, null);

            }
        }

        [HttpGet]
        public ActionResult DeleteFacilityReservation(string selectedFacResId, string secretPin)
        {
 
            int reservationId = int.Parse(selectedFacResId);

            // This is to delete reservation
            // This is success 
            if (!_authenticator.AuthenticatePin(secretPin))
            {
                TempData["Message"] = "Invalid Pin";
                return RedirectToAction("Index", "FacilityReservation");
            }
            Delete(reservationId);
            TempData["Message"] = "Deleted";
            return RedirectToAction("Index", "FacilityReservation");
        }

        [HttpPost]
        public JsonResult CheckAvailableDate([FromBody] string inputValue)
        {
            // [0]: Date, [1]:FacilityId
            string[] rawValue = inputValue.Split(',');
            DateTime startTime = Convert.ToDateTime(rawValue[0]);
            DateTime today = DateTime.Now;
            DateTime availhour = today.AddHours(1);

            IDictionary<string, string> optionList = new Dictionary<string, string>();

            // For loop to store existing facility to populate View Form DropDownList
            Dictionary<int, int> availableDateList = new Dictionary<int, int>();

            int start = 8;

            if (startTime.ToString("MM/dd/yyyy").Equals(today.ToString("MM/dd/yyyy")))
            {
                if(int.Parse(availhour.ToString("HH")) < 8)
                {
                    start = 8;
                }
                else
                {
                    start = int.Parse(availhour.ToString("HH"));
                }        
            }
            else
            {
                start = 8;
            }

            // Retrieve all facility based on Mod 3 Team 06 function
            List<PublicAreaDTO> fullfacilityList = _publicArea.getAllFacilityResults();
            PublicAreaDTO selectedFacility = null;

            foreach (var facility in fullfacilityList)
            {
                if (facility.public_area_id == int.Parse(rawValue[1]))
                {
                    selectedFacility = facility;
                    break;
                }
            }

            if (selectedFacility == null)
            {
                return null;
            }

            for (int i = start; i < 24; i++)
            {
                int slot = i * 100;
                availableDateList.Add(slot, selectedFacility.max_pax);
            }

            // This is to get all the listofFacilityRes
            IEnumerable<FacilityReservationViewModel> listFacilityRes = GetAll();
            foreach (FacilityReservationViewModel fac in listFacilityRes)
            {

                if (fac.FacilityIdDetails().Equals(int.Parse(rawValue[1])))
                {
                    if (fac.StartTimeDetails().ToString("MM/dd/yyyy").Contains(startTime.ToString("MM/dd/yyyy")))
                    {

                        for (int i = start; i < 24; i++)
                        {
                            if (fac.StartTimeDetails().ToString().Contains("AM"))
                            {

                                    if (fac.StartTimeDetails().ToString().Contains(i + ":00"))
                                    {
                                        availableDateList[i * 100] = availableDateList[i * 100] - fac.NumberOfPax();
                                        if (availableDateList[i * 100] < 0)
                                        {
                                            availableDateList[i * 100] = 0;
                                        }
                                    }
                                
                            } else if (fac.StartTimeDetails().ToString().Contains("PM"))
                            {
                                int pmCount = i;
                                if (i > 12)
                                {
                                    pmCount = i - 12;
                                    if (fac.StartTimeDetails().ToString().Contains(pmCount + ":00"))
                                    {
                                        availableDateList[i * 100] = availableDateList[i * 100] - fac.NumberOfPax();
                                        if (availableDateList[i * 100] < 0)
                                        {
                                            availableDateList[i * 100] = 0;
                                        }
                                    }
                                }
                            }
                                                    
                        }
                    }
                }
            }
            for (int i = start; i < 24; i++)
            {
                int slot = i * 100;
                string ts = "ts" + slot.ToString();
                optionList.Add(ts, availableDateList[slot].ToString());
            }

            string st = "start";
            optionList.Add(st, start.ToString());

            return Json(optionList);
        }

        /// <summary>
        /// Retrieves all existing facility reservations made by a specific guest
        /// </summary>
        /// <param name="reserveeId">   Guest if of the guest                                       </param>
        /// <returns>                   List of facility reservations represented by a view model   </returns>
        [NonAction]
        public IEnumerable<FacilityReservationViewModel> GetByReserveeID([FromRoute]int reserveeId)
        {
            IEnumerable<FacilityReservation> reservations = _facilityReservationService.RetrieveByReserveeId(reserveeId);
            List<FacilityReservationViewModel> reservationResults = new List<FacilityReservationViewModel>();
            foreach (var reservation in reservations)
                reservationResults.Add(new FacilityReservationViewModel(reservation.ReservationIdDetails(), reservation.ReserveeIdDetails()
                    , reservation.FacilityIdDetails(), reservation.NumberOfPax(), reservation.StartTimeDetails(), reservation.EndTimeDetails()));

            return reservationResults;
        }

        /// <summary>
        /// Retrieves all existing facility reservations
        /// </summary>
        /// <returns>List of facility reservations represented by a viewmodel</returns>
        [NonAction]
        public IEnumerable<FacilityReservationViewModel> GetAll()
        {
            IEnumerable<FacilityReservation> reservations = _facilityReservationService.RetrieveReservations();
            List<FacilityReservationViewModel> reservationResults = new List<FacilityReservationViewModel>();
            foreach (var reservation in reservations)
                reservationResults.Add(new FacilityReservationViewModel(reservation.ReservationIdDetails(), reservation.ReserveeIdDetails()
                    , reservation.FacilityIdDetails(), reservation.NumberOfPax(), reservation.StartTimeDetails(), reservation.EndTimeDetails()));

            return reservationResults;
        }

        /// <summary>
        /// Creates a new facility reservation
        /// </summary>
        /// <param name="reserveeId">   The guest id of the guest making the reservation    </param>
        /// <param name="facilityId">   The facility if of the facility to be reserved      </param>
        /// <param name="pax">          The total number of pax                             </param>
        /// <param name="startTime">    The start time of the reservation                   </param>
        /// <param name="endTime">      The end time of the reservation                     </param>
        /// <returns>                   Result of if the reservation creation was successful</returns>
        [NonAction]
        public bool Create([FromBody]int reserveeId, [FromBody]int facilityId, [FromBody]int pax, [FromBody]DateTime startTime, [FromBody] DateTime endTime)
        {
            if (_facilityReservationService.MakeReservation(new FacilityReservation(reserveeId, facilityId, pax, startTime, endTime)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Updates an existing facility reservation
        /// </summary>
        /// <param name="reservationId">    Id of the facility reservation to update    </param>
        /// <param name="startTime">        New start time value                        </param>
        /// <param name="endTime">          New end time value                          </param>
        /// <param name="pax">              New pax value                               </param>
        /// <returns>                       Result of if the update was successful      </returns>
        [NonAction]
        public bool Update([FromBody]int reservationId, [FromBody] DateTime startTime, [FromBody]DateTime endTime, [FromBody]int? pax = null)
        {
            FacilityReservation reservation = _facilityReservationService.RetrieveByReservationId(reservationId);
            if (reservation != null)
            {
                reservation.UpdateReservation(pax, startTime, endTime);
                if (_facilityReservationService.UpdateReservation(reservation))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Delete a facility reservation by id
        /// </summary>
        /// <param name="facilityReservationId">    Id of the facility reservation to delete        </param>
        /// <returns>                               Result of whether the deletion was successful   </returns>
        [NonAction]
        public bool Delete([FromRoute]int facilityReservationId)
        {
            return _facilityReservationService.DeleteReservation(facilityReservationId);
        }
    }
}
