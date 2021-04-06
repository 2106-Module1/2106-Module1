using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.PaymentEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HotelManagementSystem.Data.Mod2Repository
{
    /*
    * Author: Mod 2
    * Mod2Context DbContext
    */
    public class Mod2Context : DbContext
    {
        public Mod2Context(DbContextOptions<Mod2Context> options) : base(options)
        {

        }

        // Team 2 DbSets
        public DbSet<RestaurantConBooking> RestaurantConBooking { get; set; }
        public DbSet<TaxiConBooking> TaxiConBooking { get; set; }
        public DbSet<TourConBooking> TourConBooking { get; set; }
        public DbSet<ShuttleSchedule> ShuttleSchedule { get; set; }
        public DbSet<ShuttleBus> ShuttleBus { get; set; }
        public DbSet<ShuttlePassenger> ShuttlePassenger { get; set; }

        // Team 7 DbSets
        public DbSet<AbstractPayment> Payment { get; set; }
        public DbSet<ReservationInvoice> ReservationInvoice { get; set; }
        public DbSet<PostCharge> PostCharge { get; set; }
        public DbSet<ReceiptItem> ReceiptItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RestaurantConBooking>(e =>
            {
                e.HasKey("Id");
                e.Property("GuestId");
                e.Property("TransactionDateTime");
                e.Property("BookingStatus");
                e.Property("BookingType");
                e.Property("ActivityDateTime");
                e.Property("GuestName");
                e.Property("RestaurantName");
            });

            modelBuilder.Entity<TaxiConBooking>(e =>
            {
                e.HasKey("Id");
                e.Property("GuestId");
                e.Property("TransactionDateTime");
                e.Property("BookingStatus");
                e.Property("BookingType");
                e.Property("ActivityDateTime");
                e.Property("GuestName");
                e.Property("TaxiPlateNumber");
                e.Property("DriverContactNo");
            });

            modelBuilder.Entity<TourConBooking>(e =>
            {
                e.HasKey("Id");
                e.Property("GuestId");
                e.Property("TransactionDateTime");
                e.Property("BookingStatus");
                e.Property("BookingType");
                e.Property("ActivityDateTime");
                e.Property("GuestName");
                e.Property("TourPrice");
                e.Property("TourName");
            });

            modelBuilder.Entity<ShuttleSchedule>(e =>
            {
                e.HasKey("Id");
                e.Property("ScheduleDateTime");
                e.Property("TravelDirection");
                e.Property("GuestId");
                e.Property("NumberOfPassengers");
                e.Property("TransactionDateTime");
                e.Property("GuestName");
            });

            modelBuilder.Entity<ShuttleBus>(e =>
            {
                e.HasKey("Id");
                e.Property("RequisitionDateTime");
                e.Property("BusLabel");
                e.Property("NumberOfSeats");
            });

            modelBuilder.Entity<ShuttlePassenger>(e =>
            {
                e.HasKey("Id");
                e.Property("TransactionDateTime");
                e.Property("ShuttleScheduleId");
                e.Property("ShuttleBusId");
                e.Property("PassengerIndex");
            });

            // this section pre-loads rows into our database.
            // very useful for immediately debugging after cloning
            // as there are little restraints in the database, do ensure that values don't contradict each other

            // Bus Seed Data (food for thought - maybe license plate can be the id. not sure if that rolls w/ the database scaffolding though)

            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("1", "SHB1001", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("2", "SHB1002", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("3", "SHB1003", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("4", "SHB1004", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("5", "SHB1005", 12));

            // Shuttle Service Seed Data
            modelBuilder.Entity<ShuttleSchedule>().HasData(new ShuttleSchedule("111", new DateTime(2021, 3, 10, 1 , 00, 00, 000, DateTimeKind.Local), "Arrival", 9, 5, "James"));
            modelBuilder.Entity<ShuttleSchedule>().HasData(new ShuttleSchedule("112", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "Arrival", 1, 4, "Jim"));
            modelBuilder.Entity<ShuttleSchedule>().HasData(new ShuttleSchedule("113", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "Departure", 10, 5, "Bob"));

            // Shutttle Passenger Seed Data
            // Group 1 (under James)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("1", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "100", "1", "P1"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("2", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "100", "1", "P2"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("3", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "100", "1", "P3"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("4", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "100", "1", "P4"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("5", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "100", "1", "P5"));

            //Group 2 (under Jim)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("6", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "101", "1", "P6"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("7", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "101", "1", "P7"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("8", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "101", "1", "P8"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("9", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "101", "1", "P9"));

            //Group 3 (under Bob, split into two buses)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("10", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "102", "1", "P10"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("11", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "102", "1", "P11"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("12", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "102", "1", "P12"));
            //Group 3, 2nd bus (note that Bus ID is 2, instead of 1, because bus 1 is over capacity)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("13", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "102", "2", "P1"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("14", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "102", "2", "P2"));
        }
    }
}
