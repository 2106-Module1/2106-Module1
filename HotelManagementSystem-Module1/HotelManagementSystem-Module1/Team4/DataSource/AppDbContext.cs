using HotelManagementSystem_Module1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Team4.DataSource
{
    public class AppDbContext: DbContext, IAppDbContext
    {
        private DbSet<Reservation> Reservations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reservation> ReservationsDb()
        {
            return Reservations;
        }
    }
}
