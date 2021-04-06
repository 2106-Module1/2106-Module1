using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Owner of Strategy Class: Mod 1 Team 4
namespace HotelManagementSystem.Domain.Models
{
    //This is the strategy class used to generate the X-Axes and Graph Values for the Reservation Cancellation Trends
    public class CancellationTrendStrategy:IAnalyticsStrategy
    {

        private DateTime todayDate = DateTime.Now;



        int[] IAnalyticsStrategy.GenerateChartValues(IEnumerable<Reservation> ReservationList)
        {
            int[] xAxisDataArr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (var test in ReservationList)

            {
                DateTime startDate = (DateTime)test.GetReservation()["modified"];

                int monthDifference = ((todayDate.Year - startDate.Year) * 12) + todayDate.Month - startDate.Month;

                int xAxisPosition = 11 - monthDifference;

                int v = xAxisDataArr[xAxisPosition] + 1;
                xAxisDataArr[xAxisPosition] = v;
            }

            return xAxisDataArr;
        }

        String[] IAnalyticsStrategy.GenerateChartXAxis()
        {
            ArrayList xAxisMonthYear = new ArrayList();

            for (int i = 11; i > 0; i--)
            {

                DateTime insertDate = DateTime.Now.AddMonths(-i);

                String formattedXAxisString = insertDate.ToString("MMM") + "-" + insertDate.ToString("yy");
                xAxisMonthYear.Add(formattedXAxisString);

            }
            xAxisMonthYear.Add(todayDate.ToString("MMM") + "-" + todayDate.ToString("yy"));

            String[] xAxisMonthYearArr = (String[])xAxisMonthYear.ToArray(typeof(string));

            return xAxisMonthYearArr;


        }
    }
}
