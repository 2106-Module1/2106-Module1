using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner of IAnalyticsStrategy Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain.Models
{
    public interface IAnalyticsStrategy
    {
        int[] GenerateChartValues(IEnumerable<Reservation> ReservationList);
        String[] GenerateChartXAxis();
    }
}
