using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.DataSource
{
    public class ReservationGateway
    {
        private readonly IAppDbContext _appContext;

        public IEnumerable<Reservation> GetAll()
        {
            return _appContext.ReservationsDb().AsEnumerable();
        }

        public void GetByReservationId(int id)
        {
            // _appContext.ReservationsDb().SingleOrDefault(entity => entity.GetReservation()["ReservationId"] == id);
        }

        public void GetByGuestId(int id)
        {
            // _appContext.ReservationsDb().SingleOrDefault(entity => entity.GetReservation()["ReservationId"] == id);
        }

        public void Insert(Guest entity)
        {
            if (entity != null)
                _appContext.GuestsDb().Add(entity);
        }

        public void Delete(Guest entity)
        {
            if (entity != null)
                _appContext.GuestsDb().Remove(entity);
        }
    }
}
