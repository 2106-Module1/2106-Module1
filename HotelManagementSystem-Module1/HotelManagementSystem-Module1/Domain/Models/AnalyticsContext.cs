using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class AnalyticsContext
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
