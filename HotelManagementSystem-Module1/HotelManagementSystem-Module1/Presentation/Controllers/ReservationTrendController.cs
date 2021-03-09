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

            //--------------------------- CANCELLATION GRAPH ----------------------------------------------------------------

            //For creating the x-axis labels for Cancellation Graph-----------------------------------------------------

            ArrayList XAxisMonthYear = new ArrayList();

            for (int i = 11; i > 0; i--)
            {

                DateTime insertDate = DateTime.Now.AddMonths(-i);

                String formattedXAxisString = insertDate.ToString("MMM") + "-" + insertDate.ToString("yy");
                XAxisMonthYear.Add(formattedXAxisString);


            }


            XAxisMonthYear.Add(todayDate.ToString("MMM") + "-" + todayDate.ToString("yy"));

            String[] XAxisMonthYearArr = (String[])XAxisMonthYear.ToArray(typeof(string));
            ViewBag.XAxisMonthYearArr = XAxisMonthYearArr;

            //-------------------------------------------------------------------------------------------------------------

            //Generated Data for Cancelled Reservations (Both "Cancelled" and "Not Fulfilled(No Show)"-------------------

            IEnumerable<Reservation> reservationCancelledList = _reservationService.GetReservationStatusByDate("Cancelled", todayDate.AddMonths(-11), todayDate);
            IEnumerable<Reservation> reservationNoShowList = _reservationService.GetReservationStatusByDate("Not Fulfilled(No Show)", todayDate.AddMonths(-11), todayDate);

            var totalCancelledReservationList = (reservationCancelledList ?? Enumerable.Empty<Reservation>()).Concat(reservationNoShowList ?? Enumerable.Empty<Reservation>());

            IEnumerable<Reservation> reservationNotFulfilledListDateRange = _reservationService.GetReservationStatusByDate("Not Fulfilled", todayDate.AddMonths(-11), todayDate);

            int[] xAxisDataArr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (var test in totalCancelledReservationList)

            {
                DateTime startDate = (DateTime)test.GetReservation()["modified"];

                int monthDifference = ((todayDate.Year - startDate.Year) * 12) + todayDate.Month - startDate.Month;

                int xAxisPosition = 11 - monthDifference;

                int v = xAxisDataArr[xAxisPosition] + 1;
                xAxisDataArr[xAxisPosition] = v;
            }

            ViewBag.cancellationGraphData = xAxisDataArr;
            //---------------------------------------------------------------------------------------------

            //-------   CHECK-IN GRAPH------------------------------------------------------------------------

            //------------ Generating X-Axis for Check In Graph ----------------------------------------------------------------
            ArrayList XAxisCheckIn = new ArrayList();

            for (int i = 31; i >= 0; i--)
            {

                DateTime insertDate = DateTime.Now.AddDays(-i);

                String formattedXAxisString = insertDate.ToString("dd") + " - " + insertDate.ToString("MMM") + "-" + insertDate.ToString("yy");
                XAxisCheckIn.Add(formattedXAxisString);
            }

            String[] XAxisCheckInArr = (String[])XAxisCheckIn.ToArray(typeof(string));
            ViewBag.XAxisCheckInArr = XAxisCheckInArr;

            //------------------------------------------------------------------------------------------------------

            //Generated Data for Check-in Numbers (Reservation Fulfilled)----------------------------------------------------------------------
            IEnumerable<Reservation> checkedInList = _reservationService.GetReservationStatusByDate("Not Fulfilled", todayDate.AddDays(-30), todayDate);

            int[] checkInArr = new int[31];

            foreach (var res in checkedInList)
            {
                DateTime startDate = (DateTime)res.GetReservation()["start"];

                int dateDifference = (int)(todayDate - startDate).TotalDays;

                int checkInXAxisPosition = 30 - dateDifference;

                int v = checkInArr[checkInXAxisPosition] + 1;
                checkInArr[checkInXAxisPosition] = v;
            }

            ViewBag.checkInGraphData = checkInArr;

            //--------------------------------------------------------------------------------------------------------

            //--------------POPULAR ROOM GRAPH-------------------------------------------------

            //Generating X-Axis for Popular Room Graphs--------------------------------------
            String[] roomTypes = { "Double", "Twin", "Family", "Suite" };

            ViewBag.xAxisPopularRoomArr = roomTypes;

            //-----------------------------------------------------------------------------

            //Generated Data for Popular Rooms---------------------------------------------------------------------
            IEnumerable<Reservation> allReservations = _reservationService.GetAllReservations();

            int[] popularRoomTypeArr = { 0, 0, 0, 0 };

            foreach (var res in allReservations)
            {
                string roomType = (string)res.GetReservation()["roomType"];

                switch (roomType)
                {
                    case "Double":
                        int doubleRoom = popularRoomTypeArr[0] + 1;
                        popularRoomTypeArr[0] = doubleRoom;
                        break;
                    case "Twin":
                        int TwinRoom = popularRoomTypeArr[1] + 1;
                        popularRoomTypeArr[1] = TwinRoom;
                        break;
                    case "Family":
                        int FamilyRoom = popularRoomTypeArr[2] + 1;
                        popularRoomTypeArr[2] = FamilyRoom;
                        break;
                    case "Suite":
                        int SuiteRoom = popularRoomTypeArr[3] + 1;
                        popularRoomTypeArr[3] = SuiteRoom;
                        break;
                    default:
                        throw new NullReferenceException("Room Type cannot be found");
                }
            }

            ViewBag.popularRoomGraphData = popularRoomTypeArr;
            //------------------------------------------------------------------------------------------------------- 

            return View();
        }
    }
}
