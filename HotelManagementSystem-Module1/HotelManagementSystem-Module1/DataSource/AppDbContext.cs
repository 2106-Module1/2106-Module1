﻿using HotelManagementSystem.Domain.Models;
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

        private DbSet<Pin> Pin { get; set; }



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
                e.Property("role");
                e.Property("email");
            });
            modelBuilder.Entity<PromoCode>(e =>
            {
                e.HasKey("PromoCodeId");
                e.Property("PromoCodeString");
                e.Property("Discount");
            });

            //Seed data here
            // Guest Data Seed
            modelBuilder.Entity<Guest>().HasData(new Guest(1, "Scott", "Jones", "VIP", "scottj@gmail.com", "abcd1234"));
            modelBuilder.Entity<Guest>().HasData(new Guest(2, "Frank", "Guan", "VIP", "frankgj@gmail.com", "abcd1235"));
            modelBuilder.Entity<Guest>().HasData(new Guest(3, "Steven", "Wong", "Regular", "stevenwj@gmail.com", "abcd1236"));
            modelBuilder.Entity<Guest>().HasData(new Guest(4, "Indriyati", "Atmosukarto", "VIP", "indriyati@gmail.com", "ins2341234"));
            modelBuilder.Entity<Guest>().HasData(new Guest(5, "Alex", "Qiang", "VIP", "alexq@gmail.com", "aed1231235"));
            modelBuilder.Entity<Guest>().HasData(new Guest(6, "Serena", "Goh", "Regular", "serenag@gmail.com", "abc1415241"));

            // Reservation Data Seed
            modelBuilder.Entity<Reservation>().HasData(new {ReservationId = 1, ReserveGuestId = 1, NumOfGuest = 2, RoomType = "Twin",
                StartDate = DateTime.Now.AddDays(1).AddHours(14), EndDate = DateTime.Now.AddDays(2).AddHours(12), 
                Remark = "", LastModified = DateTime.Now, PromoCode = "", InitialResPrice = 2000.0, Status = "Unfulfilled" });

            modelBuilder.Entity<Reservation>().HasData(new { ReservationId = 2, ReserveGuestId = 2, NumOfGuest = 4, RoomType = "Family",
                StartDate = DateTime.Now.AddDays(1).AddHours(14), EndDate = DateTime.Now.AddDays(2).AddHours(12),
                Remark = "", LastModified = DateTime.Now, PromoCode = "", InitialResPrice = 3000.0, Status = "Unfulfilled" });

            modelBuilder.Entity<Reservation>().HasData(new { ReservationId = 3, ReserveGuestId = 3, NumOfGuest = 3, RoomType = "Suite",
                StartDate = DateTime.Now.AddDays(1).AddHours(14), EndDate = DateTime.Now.AddDays(2).AddHours(12),
                Remark = "", LastModified = DateTime.Now, PromoCode = "", InitialResPrice = 4000.0, Status = "Unfulfilled" });

            // Promo Code Data Seed
            modelBuilder.Entity<PromoCode>().HasData(new { PromoCodeId = 1, PromoCodeString = "MBSOFF20", Discount = 20 });

            modelBuilder.Entity<Pin>(e =>
            {
                e.HasKey("PinID");
                e.Property("PinNumber");
            });

            modelBuilder.Entity<Staff>().HasData(new Staff(1, "user", "0aec48126ac8511f68eabe84b2ad655c78fdba1cd8b16c3bf1ec1d4d41813dec", "Manager", "2106proj@gmail.com"));
            //password is pass1
            modelBuilder.Entity<Pin>().HasData(new Pin(1, "1234"));
            modelBuilder.Entity<Room>().HasData(new Room(1, 101, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(2, 102, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(3, 103, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(4, 104, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(5, 105, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(6, 106, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(7, 107, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(8, 108, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(9, 109, "Twin", 1000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(10, 201, "Double", 2000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(11, 202, "Double", 2000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(12, 203, "Double", 2000, 2, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(13, 626, "Family", 3000, 4, "Available", false));
            modelBuilder.Entity<Room>().HasData(new Room(14, 627, "Suite", 4000, 5, "Available", false));
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

        public DbSet<Pin> PinDB()
        {
            return Pin;
        }
    }
}