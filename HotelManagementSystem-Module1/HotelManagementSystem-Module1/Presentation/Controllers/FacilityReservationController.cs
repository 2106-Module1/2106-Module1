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

namespace HotelManagementSystem.Controllers
{
    public class FacilityReservationController : Controller
    {
        private readonly IFacilityReservationService _facilityReservationService;
        private readonly IPublicArea _publicArea;
        private readonly IGuestService _guestService;

        public FacilityReservationController(IFacilityReservationService facilityReservationService, IPublicArea publicArea,
            IGuestService guestService)
        {
            _facilityReservationService = facilityReservationService;
            _publicArea = publicArea;
            _guestService = guestService;
        }

        public ActionResult Index()
        {
            // This will return back to the view 
            //return View();
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
            String curDT = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            String oneltrDT = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm");

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
            DateTime startTime = Convert.ToDateTime(form["startTime"]);
            DateTime endTime = Convert.ToDateTime(form["endTime"]);


            // This is success scenario
            if (Create(guestId, facilityId, facilityPax, startTime, endTime))
            {
                TempData["Message"] = "Created";
                // This required to change to facilityReservation landing page.
                return RedirectToAction("Index", "FacilityReservation");
            } else
            {
                // This is unsucess scenario
            }

            return View();
        }

        [HttpGet]
        public ActionResult UpdateFacilityReservation(String selectedFacResId,String selectedGusResID)
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
            currentRecord.Add("StartTime", selectedID.StartTimeDetails().ToString("yyyy-MM-ddTHH:mm"));
            currentRecord.Add("EndTime", selectedID.EndTimeDetails().ToString("yyyy-MM-ddTHH:mm"));
            currentRecord.Add("Pax", selectedID.NumberOfPax().ToString());

            // Retrieve all existing guests
            Guest guestList = _guestService.SearchByGuestId(int.Parse(selectedGusResID));

            // For loop to store existing guest to populate View Form DropDownList
            Dictionary<int, string> existguestList = new Dictionary<int, string>();
            existguestList.Add(guestList.GuestIdDetails(), guestList.FirstNameDetails() + " " + guestList.LastNameDetails());

            // Passing data over to View Page via ViewBag
            ViewBag.currentRecord = currentRecord;
            ViewBag.existguestList = existguestList;
            ViewBag.facilityList = namefacilityList;
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
            DateTime startTime = Convert.ToDateTime(form["startTime"]);
            DateTime endTime = Convert.ToDateTime(form["endTime"]);

            // This is to delete reservation
            if (decision.Contains("Delete,Delete"))
            {
                // This is success 
                if (Delete(reservationId))
                {
                    TempData["Message"] = "Deleted";
                    // This required to change to facilityReservation landing page.
                    return RedirectToAction("Index", "FacilityReservation");
                }
            } else {
                // This is to update reservation
                // This is success scenario
                if (Update(reservationId, startTime, endTime, facilityPax))
                {
                    TempData["Message"] = "Updated";
                    // This required to change to facilityReservation landing page.
                    return RedirectToAction("Index", "FacilityReservation");
                }
            }

            return View();
        }

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

        [NonAction]
        public bool Create([FromBody]int reserveeId, [FromBody]int facilityId, [FromBody]int pax, [FromBody]DateTime startTime, [FromBody] DateTime endTime)
        {
            // I Have change the StartTime and EndTime from STRING TO DATETIME

            //if (_facilityReservationService.MakeReservation(new FacilityReservation(reserveeId, facilityId, pax
            //, new DateTime(int.Parse(startTime.Substring(0, 4)), int.Parse(startTime.Substring(4, 2)), int.Parse(startTime.Substring(4, 2)))
            //        , new DateTime(int.Parse(endTime.Substring(0, 4)), int.Parse(endTime.Substring(4, 2)), int.Parse(endTime.Substring(4, 2))));

            if (_facilityReservationService.MakeReservation(new FacilityReservation(reserveeId, facilityId, pax, startTime, endTime)))
                return true;
            else
                return false;
        }

        [NonAction]
        public bool Update([FromBody]int reservationId, [FromBody] DateTime startTime, [FromBody]DateTime endTime, [FromBody]int? pax = null)
        {
            FacilityReservation reservation = _facilityReservationService.RetrieveByReservationId(reservationId);
            if (reservation != null)
            {
                // I Have change the StartTime and EndTime from STRING TO DATETIME

                //reservation.UpdateReservation(pax
                //    , new DateTime(int.Parse(startTime.Substring(0, 4)), int.Parse(startTime.Substring(4, 2)), int.Parse(startTime.Substring(4, 2)))
                //    , new DateTime(int.Parse(endTime.Substring(0, 4)), int.Parse(endTime.Substring(4, 2)), int.Parse(endTime.Substring(4, 2))));

                reservation.UpdateReservation(pax, startTime, endTime);
                if (_facilityReservationService.UpdateReservation(reservation))
                    return true;
            }

            return false;
        }

        [NonAction]
        public bool Delete([FromRoute]int facilityReservationId)
        {
            if (_facilityReservationService.DeleteReservation(facilityReservationId))
                return true;
            else
                return false;
        }
    }
}
