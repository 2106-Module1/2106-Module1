using HotelManagementSystem_Module1.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.DataSource;

namespace HotelManagementSystem_Module1.DataSource
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private DbSet<Reservation> Reservations { get; set; }
        private DbSet<Guest> Guests { get; set; }
        private DbSet<FacilityReservation> FacilityReservations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FacilityReservation> FacilityReservationsDb()
        {
            return FacilityReservations;
        }

        public DbSet<Guest> GuestsDb()
        {
            return Guests;
        }

        public DbSet<Reservation> ReservationsDb()
        {
            return Reservations;
        }
    }
}
