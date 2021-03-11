using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain.Models;

/*
 * Owner of Reservation Repository Interface: Mod 1 Team 4
 */
namespace HotelManagementSystem.DataSource
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> GetByGuestId(int id);
        IEnumerable<Reservation> GetByStatus(string status);
        IEnumerable<Reservation> GetByTodayReservations(string status);
        IEnumerable<Reservation> GetStatusByDate(string status, DateTime start, DateTime end);
        Reservation GetLatest();
    }
}
