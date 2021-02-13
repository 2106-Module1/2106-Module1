using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.Controllers
{
    public class FacilityReservationController : Controller
    {
        private readonly FacilityReservationService _facilityReservationService;
        private readonly FacilityReservation _facilityReservation;

        public FacilityReservationController(FacilityReservationService FacilityReservationService, FacilityReservation facilityReservation)
        {
            _facilityReservationService = FacilityReservationService;
            _facilityReservation = facilityReservation;
        }

        public ActionResult Index()
        {
            // This will return back to the view 
            // Maye require to changes once view layout/design is out
            return View();
        }

        public int getByReserveeID()
        {
            return _facilityReservation.ReservationIdDetails();
        }

        public void getAll()
        {
            _facilityReservationService.RetrieveReservations();
        }

        public void create()
        {
            _facilityReservationService.MakeReservation(_facilityReservation);
        }

        public void delete()
        {
            _facilityReservationService.DeleteReservation(_facilityReservation);
        }
    }
}
