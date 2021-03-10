﻿using HotelManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.DataSource
{
    public interface IAppDbContext
    {
        public DbSet<Guest> GuestsDb();
        public DbSet<FacilityReservation> FacilityReservationsDb();
        public DbSet<Reservation> ReservationsDb();
        public DbSet<Room> RoomsDb();
        public DbSet<Staff> StaffDb();

        public DbSet<PromoCode> PromoCodesDb();

        int SaveChanges();
    }
}
