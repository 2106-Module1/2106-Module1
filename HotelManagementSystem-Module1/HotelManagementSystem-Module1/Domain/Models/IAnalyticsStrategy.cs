using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public interface IAnalyticsStrategy
    {
        int[] GenerateChartValues(IEnumerable<Reservation> ReservationList);
        String[] GenerateChartXAxis();
    }
}
