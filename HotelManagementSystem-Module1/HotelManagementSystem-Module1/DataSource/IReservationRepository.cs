using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> GetByGuestId(int id);
        IEnumerable<Reservation> GetByStatus(string status);
    }
}
