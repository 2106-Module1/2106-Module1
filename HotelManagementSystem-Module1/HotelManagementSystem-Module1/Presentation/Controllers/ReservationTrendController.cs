using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;
using HotelManagementSystem_Module1.Domain.Models;
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
            IEnumerable<Reservation> reservationCancelledList = _reservationService.GetReservationByStatus("Cancelled");
            IEnumerable<Reservation> reservationNoShowList = _reservationService.GetReservationByStatus("Not Fulfilled (No Show)");
            IEnumerable<Reservation> reservationNotFulfilledList = _reservationService.GetReservationByStatus("Not Fulfilled");

            DateTime todayDate = DateTime.Now;

            String todayMonth = todayDate.Month.ToString();
            String todayYear = todayDate.Year.ToString();

            
            int count = 0;

            ArrayList testList = new ArrayList();

            ArrayList DateList = new ArrayList();
            ArrayList XAxisMonthYear = new ArrayList();

            for(int i = 11; i > 0  ; i --){

                DateTime insertDate = DateTime.Now.AddMonths(-i);

                String formattedXAxisString = insertDate.ToString("MMM") + "-" + insertDate.ToString("yy");
                XAxisMonthYear.Add(formattedXAxisString);

                int[] monthYear = {insertDate.Month,insertDate.Year};
                DateList.Add(monthYear);
            }

            int[] currentMonthYear = { todayDate.Month, todayDate.Year };
            DateList.Add(currentMonthYear);

            foreach (var test in reservationNotFulfilledList)

            {
                DateTime modifiedDate = (DateTime)test.GetReservation()["modified"];

                if (modifiedDate.Month == todayDate.Month - 1)
                {
                    count++;
                }
                testList.Add(test.GetReservation()["status"].ToString());

            }

            ViewBag.testList = DateList;

            String[] XAxisMonthYearArr = (String[])XAxisMonthYear.ToArray(typeof(string));
            ViewBag.XAxisMonthYearArr = XAxisMonthYearArr;


            ViewBag.test = todayMonth + " " + todayYear + " " + count + " Not Fulfilled";
            return View();
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
            return View();
        }
    }
}
