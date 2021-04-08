using System;
using System.Collections.Generic;


namespace HotelManagementSystem.Domain.Models
{
    public interface IAnalyticsContext
    {
      
        public void setAnalyticsStrategy(IAnalyticsStrategy AnalyticsStrategy);


        public int[] GenerateAnalyticsChartValues(IEnumerable<Reservation> ReservationList);

        public String[] GenerateAnalyticsChartXAxis();
        
    }
}
