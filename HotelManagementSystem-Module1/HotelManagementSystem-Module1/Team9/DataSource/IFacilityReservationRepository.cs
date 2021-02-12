using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Team9.DataSource
{
    public interface IFacilityReservationRepository<T> : IRepository<T> where T: class
    {
    }
}
