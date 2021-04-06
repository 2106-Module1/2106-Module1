using HotelManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 * Used by all teams in Mod 1
 */
namespace HotelManagementSystem.DataSource
{
    /// <summary>
    /// Interface to retrieve the various DbSets from Entity framework core
    /// </summary>
    public interface IAppDbContext
    {
        /// <summary>
        /// Retrieves the guests DbSet
        /// </summary>
        public DbSet<Guest> GuestsDb();

        /// <summary>
        /// Retrieves the facility reservations DbSet
        /// </summary>
        public DbSet<FacilityReservation> FacilityReservationsDb();

        /// <summary>
        /// Retrieves the reservations DbSet
        /// </summary>
        public DbSet<Reservation> ReservationsDb();

        /// <summary>
        /// Retrieves the rooms DbSet
        /// </summary>
        public DbSet<Room> RoomsDb();

        /// <summary>
        /// Retrieves the staff DbSet
        /// </summary>
        public DbSet<Staff> StaffDb();

        /// <summary>
        /// Retrieves the promo codes DbSet
        /// </summary>
        public DbSet<PromoCode> PromoCodesDb();

        /// <summary>
        /// Retrieves the pin DbSet
        /// </summary>
        public DbSet<Pin> PinDB();

        /// <summary>
        /// Saves the changes made to the database
        /// </summary>
        int SaveChanges();
    }
}
