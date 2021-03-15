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
 * to create a Cancellation trend and Reservation Forecast.
 */
namespace HotelManagementSystem.Presentation.Controllers
{
    public class ReservationTrendController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationTrendController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /*
         * <summary>
         * Analysis Function to view Cancellation Trends
         * TODO for Deliverable 3
         * </summary>
         */
        public IActionResult ReservationTrend()
        {
            //TODO : Create a line graph to show how many cancellations by date to see a trending
            DateTime todayDate = DateTime.Now;

 

            AnalyticsContext context = new AnalyticsContext();
            context.setAnalyticsStrategy(new CancellationTrendStrategy());
            IEnumerable<Reservation> reservationCancelledList = _reservationService.GetReservationStatusByDate("Cancelled", todayDate.AddMonths(-11), todayDate);
            IEnumerable<Reservation> checkedInList = _reservationService.GetReservationStatusByDate("Not Fulfilled", todayDate.AddDays(-30), todayDate);
            IEnumerable<Reservation> allReservations = _reservationService.GetAllReservations();

            String[] xAxisMonthYearArr = context.GenerateAnalyticsChartXAxis();
            int[] xAxisDataArr = context.GenerateAnalyticsChartValues(reservationCancelledList);

            
            ViewBag.xAxisMonthYearArr = xAxisMonthYearArr;
            ViewBag.cancellationGraphData = xAxisDataArr;


            context.setAnalyticsStrategy(new CheckInTrendStrategy());

            String[] XAxisCheckInArr = context.GenerateAnalyticsChartXAxis();
            int[] checkInArr = context.GenerateAnalyticsChartValues(checkedInList);
            ViewBag.XAxisCheckInArr = XAxisCheckInArr;
            ViewBag.checkInGraphData = checkInArr;

            context.setAnalyticsStrategy(new PopularRoomGraphStrategy());
            String[] xAxisPopularRoomArr = context.GenerateAnalyticsChartXAxis();
            int[] popularRoomTypeArr = context.GenerateAnalyticsChartValues(allReservations);

            ViewBag.xAxisPopularRoomArr = xAxisPopularRoomArr;
            ViewBag.popularRoomGraphData = popularRoomTypeArr;


            return View();
        }
    }
}
