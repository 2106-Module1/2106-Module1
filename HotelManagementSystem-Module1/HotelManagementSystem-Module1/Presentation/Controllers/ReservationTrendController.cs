using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


/*
 * Owner of ReservationManagementController: Mod 1 Team 4
 * This Controller is used for Manipulated and Analysis All Reservations
 * to create a Cancellation, Reservation Forecast and also overview on Popular Room Types.
 */
namespace HotelManagementSystem.Presentation.Controllers
{
    public class ReservationTrendController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IAnalyticsContext _analyticsContext;

        public ReservationTrendController(IReservationService reservationService, IAnalyticsContext analyticsContext)
        {
            _reservationService = reservationService;
            _analyticsContext = analyticsContext;
        }
        
        /// <summary>
        /// (Completed)
        /// Analysis Function to view Cancellation and Check-in Trends and also an overview on Popular Room Types
        /// </summary>
        public IActionResult ReservationTrend()
        {
          
            DateTime todayDate = DateTime.Now;

            
            //List of Reservations which are cancelled from 1 Year before till today (For Cancellation Trends)
            IEnumerable<Reservation> reservationCancelledList = _reservationService.GetReservationStatusByDate("Cancelled", todayDate.AddMonths(-11), todayDate); 

            //List of Upcoming Check-in in the Upcoming Month (For Check-in Trends)
            IEnumerable<Reservation> checkedInList = _reservationService.GetReservationStatusByDate("Unfulfilled", todayDate, todayDate.AddDays(30));


            //List of All Reservations Available (For Popular Room Graph)
            IEnumerable<Reservation> allReservations = _reservationService.GetAllReservations();


           

            //Instantiating Strategy for generating graph for Cancellation Trends
            _analyticsContext.setAnalyticsStrategy(new CancellationTrendStrategy());

            String[] xAxisMonthYearArr = _analyticsContext.GenerateAnalyticsChartXAxis();
            int[] xAxisDataArr = _analyticsContext.GenerateAnalyticsChartValues(reservationCancelledList);

            //Sending array of X-Axis Values and Graph Data into the ViewBag for Cancellation Graph Generation in Reservation Analytic View
            ViewBag.xAxisMonthYearArr = xAxisMonthYearArr;
            ViewBag.cancellationGraphData = xAxisDataArr;


            //Changed the strategy of the context to the Check In Trend Chart Strategy
            _analyticsContext.setAnalyticsStrategy(new CheckInTrendStrategy());

            String[] XAxisCheckInArr = _analyticsContext.GenerateAnalyticsChartXAxis();
            int[] checkInArr = _analyticsContext.GenerateAnalyticsChartValues(checkedInList);

            //Sending array of X-Axis Values and Graph Data for Check-in Chart into ViewBag for Check-in Graph Generation in Reservation Analytic View
            ViewBag.XAxisCheckInArr = XAxisCheckInArr;
            ViewBag.checkInGraphData = checkInArr;


            //Change the strategy of the context to the Popular Room Graph Strategy
            _analyticsContext.setAnalyticsStrategy(new PopularRoomGraphStrategy());

            String[] xAxisPopularRoomArr = _analyticsContext.GenerateAnalyticsChartXAxis();
            int[] popularRoomTypeArr = _analyticsContext.GenerateAnalyticsChartValues(allReservations);

            //Sending array of X-Axis Values and Graph Data for Check-in Chart into ViewBag for Popular Room Graphs Generation in Reservation Analytic View
            ViewBag.xAxisPopularRoomArr = xAxisPopularRoomArr;
            ViewBag.popularRoomGraphData = popularRoomTypeArr;


            return View();
        }
    }
}
