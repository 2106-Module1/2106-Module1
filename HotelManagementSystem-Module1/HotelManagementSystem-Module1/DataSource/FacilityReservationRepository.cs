using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.DataSource
{
    public class FacilityReservationRepository : IFacilityReservationRepository
    {
        private readonly IAppDbContext _appContext;

        public FacilityReservationRepository(IAppDbContext appContext)
        {
            _appContext = appContext;
        }

        public void Delete(FacilityReservation entity)
        {
            if (entity != null)
            {
                _appContext.FacilityReservationsDb().Remove(entity);
                _appContext.SaveChanges();
            }
        }

        public IEnumerable<FacilityReservation> GetAll()
        {
            return _appContext.FacilityReservationsDb().AsEnumerable();
        }

        public FacilityReservation GetById(int id)
        {
            return _appContext.FacilityReservationsDb().AsEnumerable().SingleOrDefault(entity => entity.ReservationIdDetails() == id);
        }

        public IEnumerable<FacilityReservation> GetByReserveeId(int reserveeId)
        {
            return _appContext.FacilityReservationsDb().AsEnumerable().Where(entity => entity.ReserveeIdDetails() == reserveeId);
        }

        public void Insert(FacilityReservation entity)
        {
            if (entity != null)
            {
                _appContext.FacilityReservationsDb().Add(entity);
                _appContext.SaveChanges();
            }
        }

        public void Update(FacilityReservation entity)
        {
            if (entity != null)
            {
                _appContext.FacilityReservationsDb().Update(entity);
                _appContext.SaveChanges();
            }
        }
    }
}
