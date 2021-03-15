using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Owner of Reservation Repository: Mod 1 Team 4
 */
namespace HotelManagementSystem.DataSource
{
    public class ReservationRepository : IReservationRepository
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

        public Reservation GetById(int id)
        {
            return _appContext.ReservationsDb().AsEnumerable().SingleOrDefault(entity => (int)(entity.GetReservation()["resID"]) == id);
        }

        public IEnumerable<Reservation> GetByGuestId(int id)
        {
            return _appContext.ReservationsDb().AsEnumerable().Where(entity => (int)(entity.GetReservation()["guestID"]) == id);
        }

        /*public IEnumerable<Reservation> GetByStatus(string status)
        {
            return _appContext.ReservationsDb().AsEnumerable().Where(entity => (string)(entity.GetReservation()["status"]) == status);
        }*/

        public IEnumerable<Reservation> GetByTodayReservations(string status)
        {
            return _appContext.ReservationsDb().AsEnumerable().Where(entity =>
                ((DateTime)(entity.GetReservation()["start"])).Date == DateTime.Now.Date &&
                (string)(entity.GetReservation()["status"]) == status);
        }

        public IEnumerable<Reservation> GetStatusByDate(string status, DateTime Start, DateTime End)
        {
            return _appContext.ReservationsDb().AsEnumerable().Where(entity => ((string)(entity.GetReservation()["status"]) == status) &&
                                                                               (DateTime)(entity.GetReservation()["modified"]) >= Start &&
                                                                               (DateTime)(entity.GetReservation()["modified"]) <= End);
        }

        public Reservation GetLatest()
        {
            return _appContext.ReservationsDb().AsEnumerable().OrderByDescending(entity => entity.GetReservation()["resID"]).FirstOrDefault();
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

        public void Update(Reservation entity)
        {
            if (entity != null)
            {
                _appContext.ReservationsDb().Update(entity);
                _appContext.SaveChanges();
            }
        }
    }
}
