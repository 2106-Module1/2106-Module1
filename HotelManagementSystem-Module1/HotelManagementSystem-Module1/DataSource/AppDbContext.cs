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

        public DbSet<PromoCode> PromoCodesDb()
        {
            return PromoCodes;
        }
    }
}