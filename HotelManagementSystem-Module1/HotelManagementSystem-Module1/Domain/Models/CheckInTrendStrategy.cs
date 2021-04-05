using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class CheckInTrendStrategy : IAnalyticsStrategy
    {

        private DateTime todayDate = DateTime.Now;


        int[] IAnalyticsStrategy.GenerateChartValues(IEnumerable<Reservation> ReservationList)
        {
            int[] checkInArr = new int[31];

            foreach (var res in ReservationList)
            {
                DateTime startDate = (DateTime)res.GetReservation()["start"];

                int dateDifference = (int)(todayDate - startDate).TotalDays;

                int checkInXAxisPosition = 30 - dateDifference;

                int v = checkInArr[checkInXAxisPosition] + 1;
                checkInArr[checkInXAxisPosition] = v;
            }

            return checkInArr;
        }

        string[] IAnalyticsStrategy.GenerateChartXAxis()
        {
            ArrayList XAxisCheckIn = new ArrayList();

            for (int i = 31; i >= 0; i--)
            {

                DateTime insertDate = DateTime.Now.AddDays(-i);

                String formattedXAxisString = insertDate.ToString("dd") + " - " + insertDate.ToString("MMM") + "-" + insertDate.ToString("yy");
                XAxisCheckIn.Add(formattedXAxisString);
            }

            String[] XAxisCheckInArr = (String[])XAxisCheckIn.ToArray(typeof(string));
            return XAxisCheckInArr;
        }
    }
}
