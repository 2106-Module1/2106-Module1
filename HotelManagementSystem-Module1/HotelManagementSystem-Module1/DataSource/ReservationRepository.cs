using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.DataSource
{
    public class ReservationRepository
    {
        private readonly IAppDbContext _appContext;

        public ReservationRepository(IAppDbContext appContext)
        {
            _appContext = appContext;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _appContext.ReservationsDb().AsEnumerable();
        }

        public void GetById(int id)
        {
            // _appContext.ReservationsDb().SingleOrDefault(entity => entity.GetReservation()["ReservationId"] == id);
        }

        public void GetByGuestId(int id)
        {
            // _appContext.ReservationsDb().SingleOrDefault(entity => entity.GetReservation()["ReservationId"] == id);
        }
        

        public void Insert(Reservation entity)
        {
            if (entity != null)
            {
                _appContext.ReservationsDb().Add(entity);
                _appContext.SaveChanges();
            }
        }

        public void Delete(Reservation entity)
        {
            if (entity != null)
            {
                _appContext.ReservationsDb().Remove(entity);
                _appContext.SaveChanges();
            }
        }
    }
}
