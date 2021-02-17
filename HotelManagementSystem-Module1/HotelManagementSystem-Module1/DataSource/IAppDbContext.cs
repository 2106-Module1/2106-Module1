using HotelManagementSystem_Module1.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.DataSource
{
    public interface IAppDbContext
    {
        public DbSet<Guest> GuestsDb();
        public DbSet<FacilityReservation> FacilityReservationsDb();
        public DbSet<Reservation> ReservationsDb();
        public DbSet<Room> RoomsDb();
        int SaveChanges();
    }
}
