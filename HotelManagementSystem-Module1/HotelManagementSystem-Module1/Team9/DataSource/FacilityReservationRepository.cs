using HotelManagementSystem_Module1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Team9.DataSource
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
                _appContext.FacilityReservationsDb().Remove(entity);
        }

        public IEnumerable<FacilityReservation> GetAll()
        {
            return _appContext.FacilityReservationsDb().AsEnumerable();
        }

        public FacilityReservation GetById(int id)
        {
            return _appContext.FacilityReservationsDb().SingleOrDefault(entity => entity.FacilityIdDetails() == id);
        }

        public void Insert(FacilityReservation entity)
        {
            if (entity != null)
                _appContext.FacilityReservationsDb().Add(entity);
        }

        public void Update(FacilityReservation entity)
        {
            if (entity != null)
                _appContext.FacilityReservationsDb().Update(entity);
        }
    }
}
