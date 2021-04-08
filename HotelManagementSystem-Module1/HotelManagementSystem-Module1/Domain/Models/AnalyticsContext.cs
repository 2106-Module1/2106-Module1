using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner of AnalyticsContext: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    //This is the context class which connects all the Strategy Classes together. The client calls each strategy and its methods through this context class
    public class AnalyticsContext:IAnalyticsContext
    {
        public IAnalyticsStrategy anaStrategy;

        public void setAnalyticsStrategy(IAnalyticsStrategy AnalyticsStrategy)
        {
            anaStrategy = AnalyticsStrategy;
        }

        public int[] GenerateAnalyticsChartValues(IEnumerable<Reservation> ReservationList)
        {
            return anaStrategy.GenerateChartValues(ReservationList);
        }
        public String[] GenerateAnalyticsChartXAxis()
        {
            return anaStrategy.GenerateChartXAxis();
        }
    }
}
