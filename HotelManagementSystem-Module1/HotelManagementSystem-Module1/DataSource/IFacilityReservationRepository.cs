using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.DataSource
{
    public interface IFacilityReservationRepository : IRepository<FacilityReservation>
    {
        IEnumerable<FacilityReservation> GetByReserveeId(int reserveeId);
    }
}
