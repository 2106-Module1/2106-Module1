using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Mvc;

/*
 * Owner of ReservationManagementController: Mod 1 Team 4
 * This Controller is used for Updating Reservations only.
 */
namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    /*
     * <summary>
     * Controller to Route Update function to update reservation details.
     * </summary>
     */
    public class ReservationManagementController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IGuestService _guestService;
        /*
         * To include after D2
         * private readonly IAuthenticate _authenticationService
         */

        /*
         * Implementing code together with Mod 1 Team 6 Authentication Service
         * public ReservationManagementController(IReservationService reservationService, IGuestService guestService, IAuthenticate authenticateService
         */
        public ReservationManagementController(IReservationService reservationService, IGuestService guestService)
        {
            _guestService = guestService;
            _reservationService = reservationService;
            /*
             * Calling Mod 1 Team 6 Service - for authentication of secret pin
             * _authenticationService = authenticateService;
             */
        }
        /*
         * <summary>
         * Update Function to update reservation details.
         * TODO for Deliverable 3
         * </summary>
         */
        [NonAction]
        public IActionResult UpdateReservation()
        {
            //TODO : Update Reservation Details
            throw new NotImplementedException();
        }
    }
}
