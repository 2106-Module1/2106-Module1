using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Owner of Strategy Class: Mod 1 Team 4
namespace HotelManagementSystem.Domain.Models
{
    //This is the strategy class used to generate the X-Axes and Graph Values for the Reservation Check-In Trends
    public class CheckInTrendStrategy : IAnalyticsStrategy
    {

        private DateTime todayDate = DateTime.Now;


        int[] IAnalyticsStrategy.GenerateChartValues(IEnumerable<Reservation> ReservationList)
        {
            int[] checkInArr = new int[31];

            foreach (var res in ReservationList)
            {
                DateTime startDate = (DateTime)res.GetReservation()["start"];

                int dateDifference = (int)(startDate - todayDate).TotalDays;


                int v = checkInArr[dateDifference] + 1;
                checkInArr[dateDifference] = v;
            }

            return checkInArr;
        }

        string[] IAnalyticsStrategy.GenerateChartXAxis()
        {
            ArrayList XAxisCheckIn = new ArrayList();

            for (int i = 0; i <= 31; i++)
            {

                DateTime insertDate = DateTime.Now.AddDays(i);

                String formattedXAxisString = insertDate.ToString("dd") + " - " + insertDate.ToString("MMM") + "-" + insertDate.ToString("yy");
                XAxisCheckIn.Add(formattedXAxisString);
            }

            String[] XAxisCheckInArr = (String[])XAxisCheckIn.ToArray(typeof(string));
            return XAxisCheckInArr;
        }
    }
}
