using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.DataSource
{
    public class GuestRepository : IGuestRepository
    {
        private readonly IAppDbContext _appContext;

        public GuestRepository(IAppDbContext appContext)
        {
            _appContext = appContext;
        }

        public void Delete(Guest entity)
        {
            if (entity != null)
                _appContext.GuestsDb().Remove(entity);
        }

        public IEnumerable<Guest> GetAll()
        {
            return _appContext.GuestsDb().AsEnumerable();
        }

        public Guest GetById(int id)
        {
            return _appContext.GuestsDb().SingleOrDefault(entity => entity.GuestIdDetails() == id);
        }

        public IEnumerable<Guest> GetByName(string name)
        {
            return _appContext.GuestsDb().Where(entity => entity.FullName().Contains(name));
        }

        public IEnumerable<Guest> GetByPassportNumber(string passportNumber)
        {
            return _appContext.GuestsDb().Where(entity => entity.PassportNumberDetails().Contains(passportNumber));
        }

        public void Insert(Guest entity)
        {
            if (entity != null)
                _appContext.GuestsDb().Add(entity);
        }

        public void Update(Guest entity)
        {
            if (entity != null)
                _appContext.GuestsDb().Update(entity);
        }
    }
}
