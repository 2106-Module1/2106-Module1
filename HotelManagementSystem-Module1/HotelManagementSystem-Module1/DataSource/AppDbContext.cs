using HotelManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.DataSource;
using System.Reflection;

namespace HotelManagementSystem.DataSource
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private DbSet<Reservation> Reservations { get; set; }
        private DbSet<Guest> Guests { get; set; }
        private DbSet<FacilityReservation> FacilityReservations { get; set; }
        private DbSet<Room> Rooms { get; set; }

        private DbSet<Staff> Staff { get; set; }

        private DbSet<PromoCode> PromoCodes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FacilityReservation>(e =>
            {
                e.HasKey("FacilityReservationId");
                e.Property("ReserveeGuestId");
                e.Property("FacilityId");
                e.Property("Pax");
                e.Property("StartTime");
                e.Property("EndTime");
                e.Property("Cancelled");
            });

            modelBuilder.Entity<Guest>(e =>
            {
                e.HasKey("GuestId");
                e.Property("FirstName");
                e.Property("LastName");
                e.Property("GuestType");
                e.Property("Email");
                e.Property("PassportNumber");
                e.Property("OutstandingCharges");
            });

            modelBuilder.Entity<Reservation>(e =>
            {
                e.HasKey("ReservationId");
                e.Property("ReserveGuestId");
                e.Property("NumOfGuest");
                e.Property("RoomType");
                e.Property("StartDate");
                e.Property("EndDate");
                e.Property("Remark");
                e.Property("LastModified");
                e.Property("PromoCode");
                e.Property("InitialResPrice");
                e.Property("Status");
            });

            modelBuilder.Entity<Room>(e =>
            {
                e.HasKey("roomID");
                e.Property("roomNumber");
                e.Property("roomType");
                e.Property("roomPrice");
                e.Property("roomCapacity");
                e.Property("roomStatus");
                e.Property("isSmoking");
            });

            modelBuilder.Entity<Staff>(e =>
            {
                e.HasKey("staffID");
                e.Property("username");
                e.Property("password");
                e.Property("staffRole");
                e.Property("pin");
            });
            modelBuilder.Entity<PromoCode>(e =>
            {
                e.HasKey("PromoCodeId");
                e.Property("PromoCodeString");
                e.Property("Discount");
            });

            //Seed data here
            modelBuilder.Entity<Guest>().HasData(new Guest(1, "Scott", "Jones", "VIP", "scottj@gmail.com", "abcd1234"));
            modelBuilder.Entity<Guest>().HasData(new Guest(2, "Frank", "Guan", "VIP", "frankgj@gmail.com", "abcd1235"));
            modelBuilder.Entity<Guest>().HasData(new Guest(3, "Steven", "Wong", "Regular", "stevenwj@gmail.com", "abcd1236"));

            modelBuilder.Entity<Room>().HasData(new Room(1, 201, "Double", 1000.0, 2, "Empty", false));
            modelBuilder.Entity<Room>().HasData(new Room(2, 202, "Twin", 2000.0, 2, "Empty", false));
            modelBuilder.Entity<Room>().HasData(new Room(3, 203, "Family", 3000.0, 4, "Empty", false));
            modelBuilder.Entity<Room>().HasData(new Room(4, 204, "Suite", 4000.0, 5, "Empty", false));

            // TO CLEAR BEFORE D4 SUBMISSION!!!
            modelBuilder.Entity<Reservation>().HasData(new Reservation(1, 1, 2, "Twin",
                new DateTime(2021, 4, 3, 10, 04, 00, DateTimeKind.Local), 
                new DateTime(2021, 4, 5, 12, 00, 00, DateTimeKind.Local), 
                "", DateTime.Now, "",2000.0, "Unfulfilled"));
            modelBuilder.Entity<Reservation>().HasData(new Reservation(2, 2, 3, "Family",
                new DateTime(2021, 4, 3, 10, 04, 00, DateTimeKind.Local),
                new DateTime(2021, 4, 5, 12, 00, 00, DateTimeKind.Local),
                "", DateTime.Now, "", 3000.0, "Unfulfilled"));
            modelBuilder.Entity<Reservation>().HasData(new Reservation(3, 3, 2, "Suite",
                new DateTime(2021, 4, 3, 10, 04, 00, DateTimeKind.Local),
                new DateTime(2021, 4, 5, 12, 00, 00, DateTimeKind.Local),
                "", DateTime.Now, "", 4000.0, "Unfulfilled"));
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

        public DbSet<Room> RoomsDb()
        {
            return Rooms;
        }

        public DbSet<Staff> StaffDb()
        {
            return Staff;
        }
        public DbSet<PromoCode> PromoCodesDb()
        {
            return PromoCodes;
        }
    }
}