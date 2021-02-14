using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.Presentation.ViewModels;

namespace HotelManagementSystem_Module1.Controllers
{
    public class FacilityReservationController : Controller
    {
        private readonly IFacilityReservationService _facilityReservationService;

        public FacilityReservationController(IFacilityReservationService facilityReservationService)
        {
            _facilityReservationService = facilityReservationService;
        }

        public ActionResult Index()
        {
            // This will return back to the view 
            // Maye require to changes once view layout/design is out
            return View();
        }

        [HttpGet("GetByReserveeId/{reserveeId}")]
        public ActionResult<IEnumerable<FacilityReservationViewModel>> GetByReserveeID([FromRoute]int reserveeId)
        {
            IEnumerable<FacilityReservation> reservations = _facilityReservationService.RetrieveByReserveeId(reserveeId);
            List<FacilityReservationViewModel> reservationResults = new List<FacilityReservationViewModel>();
            foreach (var reservation in reservations)
                reservationResults.Add(new FacilityReservationViewModel(reservation.ReservationIdDetails(), reservation.ReserveeIdDetails()
                    , reservation.FacilityIdDetails(), reservation.NumberOfPax(), reservation.StartTimeDetails(), reservation.EndTimeDetails()));

            return reservationResults;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<FacilityReservationViewModel>> GetAll()
        {
            IEnumerable<FacilityReservation> reservations = _facilityReservationService.RetrieveReservations();
            List<FacilityReservationViewModel> reservationResults = new List<FacilityReservationViewModel>();
            foreach (var reservation in reservations)
                reservationResults.Add(new FacilityReservationViewModel(reservation.ReservationIdDetails(), reservation.ReserveeIdDetails()
                    , reservation.FacilityIdDetails(), reservation.NumberOfPax(), reservation.StartTimeDetails(), reservation.EndTimeDetails()));

            return reservationResults;
        }

        [HttpPost("NewGuest")]
        public IActionResult Create([FromBody]int reserveeId, [FromBody]int facilityId, [FromBody]int pax, [FromBody]string startTime, [FromBody]string endTime)
        {
            if (_facilityReservationService.MakeReservation(new FacilityReservation(reserveeId, facilityId, pax
                , new DateTime(int.Parse(startTime.Substring(0, 4)), int.Parse(startTime.Substring(4, 2)), int.Parse(startTime.Substring(4, 2)))
                , new DateTime(int.Parse(endTime.Substring(0, 4)), int.Parse(endTime.Substring(4, 2)), int.Parse(endTime.Substring(4, 2))))))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("UpdateGuest")]
        public IActionResult Update([FromBody]int reservationId, [FromBody]string startTime, [FromBody]string endTime, [FromBody]int? pax = null)
        {
            FacilityReservation reservation = _facilityReservationService.RetrieveByReservationId(reservationId);
            if (reservation != null)
            {
                reservation.UpdateReservation(pax
                    , new DateTime(int.Parse(startTime.Substring(0, 4)), int.Parse(startTime.Substring(4, 2)), int.Parse(startTime.Substring(4, 2)))
                    , new DateTime(int.Parse(endTime.Substring(0, 4)), int.Parse(endTime.Substring(4, 2)), int.Parse(endTime.Substring(4, 2))));
                if (_facilityReservationService.UpdateReservation(reservation))
                    return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("DeleteGuest/{facilityReservationId}")]
        public IActionResult Delete([FromRoute]int facilityReservationId)
        {
            if (_facilityReservationService.DeleteReservation(facilityReservationId))
                return Ok();
            else
                return BadRequest();
        }
    }
}
