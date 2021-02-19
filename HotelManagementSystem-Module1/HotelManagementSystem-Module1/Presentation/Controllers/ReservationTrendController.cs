using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Mvc;

/*
 * Owner of ReservationManagementController: Mod 1 Team 4
 * This Controller is used for Manipulated and Analysis All Reservations
 * to create a Cancellation trend and Reservation Forecast.
 */
namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class ReservationTrendController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IGuestService _guestService;

        public ReservationTrendController(IReservationService reservationService, IGuestService guestService)
        {
            _guestService = guestService;
            _reservationService = reservationService;
        }

        /*
         * <summary>
         * Analysis Function to view Cancellation Trends
         * TODO for Deliverable 3
         * </summary>
         */
        public IActionResult CancellationTrend()
        {
            //TODO : Create a line graph to show how many cancellations by date to see a trending
            throw new NotImplementedException();
        }

        /*
         * <summary>
         * Analysis Function to view Check in Forecast Trends
         * This is to better prepare Managers to allocate staffing properly during high check in forecast
         * TODO for Deliverable 3
         * </summary>
         */
        public IActionResult CheckInTrend()
        {
            //TODO : Create a line graph to show how many check in reservations by date
            throw new NotImplementedException();
        }
    }
}
