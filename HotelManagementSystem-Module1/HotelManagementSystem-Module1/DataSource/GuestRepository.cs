﻿using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.DataSource
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
            {
                _appContext.GuestsDb().Remove(entity);
                _appContext.SaveChanges();
            }
        }

        public IEnumerable<Guest> GetAll()
        {
            return _appContext.GuestsDb().AsEnumerable();
        }

        public Guest GetById(int id)
        {
            return _appContext.GuestsDb().AsEnumerable().SingleOrDefault(entity => entity.GuestIdDetails() == id);
        }

        public IEnumerable<Guest> GetByName(string name)
        {
            return _appContext.GuestsDb().AsEnumerable().Where(entity => entity.FirstNameDetails().Contains(name) || entity.LastNameDetails().Contains(name));
        }

        public IEnumerable<Guest> GetByPassportNumber(string passportNumber)
        {
            return _appContext.GuestsDb().AsEnumerable().Where(entity => entity.PassportNumberDetails().Contains(passportNumber));
        }

        public void Insert(Guest entity)
        {
            if (entity != null)
            {
                _appContext.GuestsDb().Add(entity);
                _appContext.SaveChanges();
            }
        }

        public void Update(Guest entity)
        {
            if (entity != null)
            {
                _appContext.GuestsDb().Update(entity);
                _appContext.SaveChanges();
            }
        }
    }
}
