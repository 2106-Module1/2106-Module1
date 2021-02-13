using HotelManagementSystem_Module1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Team4.DataSource
{
    public interface IAppDbContext
    {
        public DbSet<Reservation> ReservationsDb();
    }
}
