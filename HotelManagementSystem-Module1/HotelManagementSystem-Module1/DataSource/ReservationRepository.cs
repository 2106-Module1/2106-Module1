using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

/*
 * Owner of Reservation Repository: Mod 1 Team 4
 */
namespace HotelManagementSystem_Module1.DataSource
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

        public Reservation GetLatest()
        {
            return _appContext.ReservationsDb().AsEnumerable().OrderByDescending(entity => entity.GetReservation()["resID"]).FirstOrDefault();
        }

        public Reservation GetById(int id)
        {
            return _appContext.ReservationsDb().AsEnumerable().SingleOrDefault(entity => (int)(entity.GetReservation()["resID"]) == id);
        }

        public IEnumerable<Reservation> GetByGuestId(int id)
        {
            return _appContext.ReservationsDb().AsEnumerable().Where(entity => (int)(entity.GetReservation()["guestID"]) == id);
        }

        public IEnumerable<Reservation> GetByStatus(string status)
        {
            return _appContext.ReservationsDb().AsEnumerable().Where(entity => (string)(entity.GetReservation()["status"]) == status);
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
