using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.PaymentEntities;
using Microsoft.EntityFrameworkCore;
using System;

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

        // Team 1 DbSets
        /*
        public DbSet<CheckIn> CheckIn { get; set; }
        public DbSet<RoomRequest> RoomRequest { get; set; }
        public DbSet<RoomTransferRequest> RoomTransferRequest { get; set; }
        public DbSet<RoomUpgradeRequest> RoomUpgradeRequest { get; set; }
        public DbSet<KeyCard> KeyCard { get; set; }
        public DbSet<MissingReport> MissingReport { get; set; }
        public DbSet<DamagedReport> DamagedReport { get; set; }
        public DbSet<IncidentReport> IncidentReport { get; set; }
        */
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
            modelBuilder.Entity<AbstractPayment>()
               .HasDiscriminator(p => p.PaymentType)
               .HasValue<PostCharge>("PostCharge")
               .HasValue<ReservationInvoice>("ReservationInvoice");
            /*
            modelBuilder.Entity<KeyCard>(e =>
               {
                   e.HasKey("keyID");
                   e.Property("roomID");
                   e.Property("remarks");
               }
            );
            modelBuilder.Entity<DamagedReport>(e =>
            {
                e.HasKey("Report_ID");
                e.Property("ReservationId");
                e.Property("Cost");
                e.Property("No_Keys");
                e.Property("Report_Description");
                e.Property("Report_Type");
            }
            );
            modelBuilder.Entity<MissingReport>(e =>
            {
                e.HasKey("Report_ID");
                e.Property("ReservationId");
                e.Property("Cost");
                e.Property("No_Keys");
                e.Property("Report_Description");
                e.Property("Report_Type");
            }
           );

            modelBuilder.Entity<CheckIn>(e =>
            {
                e.HasKey("CheckIn_id");
                e.Property("Reservation_id");
                e.Property("Guest_id");
                e.Property("Room_id");
                e.Property("Date");
                e.Property("CheckedIn_Status");
                e.Property("Reservee");
            });
            */

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
                e.Property("State");
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

            /*
            modelBuilder.Entity<CheckIn>(e =>
            {
                e.HasKey("CheckIn_id");
                e.Property("Reservation_id");
                e.Property("Guest_id");
                e.Property("Room_id");
                e.Property("Date");
                e.Property("CheckedIn_Status");
            });

            // this section pre-loads rows into our database.
            // very useful for immediately debugging after cloning
            // as there are little restraints in the database, do ensure that values don't contradict each other

            // Team 1 Seeding (Temp did up for team 2 and7 to test functions)
            CheckIn data1 = new CheckIn();
            data1.CheckIn_id = 1;
            data1.Reservation_id = 1;
            data1.Guest_id = 1;
            data1.Room_id = 1;
            data1.Date = new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local);
            data1.CheckedIn_Status = true;
            data1.Reservee = true;

            CheckIn data2 = new CheckIn();
            data2.CheckIn_id = 2;
            data2.Reservation_id = 2;
            data2.Guest_id = 2;
            data2.Room_id = 2;
            data2.Date = new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local);
            data2.CheckedIn_Status = true;
            data2.Reservee = true;

            CheckIn data3 = new CheckIn();
            data3.CheckIn_id = 3;
            data3.Reservation_id = 3;
            data3.Guest_id = 3;
            data3.Room_id = 3;
            data3.Date = new DateTime(2022, 3, 10, 1, 00, 00, 000, DateTimeKind.Local);
            data3.CheckedIn_Status = true;
            data3.Reservee = true;


            modelBuilder.Entity<CheckIn>().HasData(data1);
            modelBuilder.Entity<CheckIn>().HasData(data2);
            modelBuilder.Entity<CheckIn>().HasData(data3);
            */

            // Team 2 Seeding
            // Bus Seed Data (food for thought - maybe license plate can be the id. not sure if that rolls w/ the database scaffolding though)
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("1", "SHB1001", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("2", "SHB1002", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("3", "SHB1003", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("4", "SHB1004", 12));
            modelBuilder.Entity<ShuttleBus>().HasData(new ShuttleBus("5", "SHB1005", 12));

            // Shuttle Service Seed Data
            modelBuilder.Entity<ShuttleSchedule>().HasData(new ShuttleSchedule("111", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "Arrival", 9, 5, "James", "CREATED"));
            modelBuilder.Entity<ShuttleSchedule>().HasData(new ShuttleSchedule("112", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "Arrival", 1, 4, "Jim", "CREATED"));
            modelBuilder.Entity<ShuttleSchedule>().HasData(new ShuttleSchedule("113", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "Departure", 10, 5, "Bob", "CREATED"));

            // Shutttle Passenger Seed Data
            // Group 1 (under James)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("1", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "111", "1", "P1"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("2", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "111", "1", "P2"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("3", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "111", "1", "P3"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("4", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "111", "1", "P4"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("5", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "111", "1", "P5"));

            //Group 2 (under Jim)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("6", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "112", "1", "P6"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("7", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "112", "1", "P7"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("8", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "112", "1", "P8"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("9", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "112", "1", "P9"));

            //Group 3 (under Bob, split into two buses)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("10", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "113", "1", "P10"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("11", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "113", "1", "P11"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("12", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "113", "1", "P12"));
            //Group 3, 2nd bus (note that Bus ID is 2, instead of 1, because bus 1 is over capacity)
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("13", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "113", "2", "P1"));
            modelBuilder.Entity<ShuttlePassenger>().HasData(new ShuttlePassenger("14", new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local), "113", "2", "P2"));

            // Conbooking seeding
            modelBuilder.Entity<TaxiConBooking>().HasData(new TaxiConBooking().SetTaxiBooking("C2021040212341", 1, "BOOKED", "Taxi",
            new DateTime(2021, 4, 10, 1, 01, 00, 000, DateTimeKind.Local), "SGU1234E", 87654321, "Scott Jones"));

            modelBuilder.Entity<RestaurantConBooking>().HasData(new RestaurantConBooking().SetRestaurantBooking("C2021040213021", 1, "BOOKED", "Restaurant",
                new DateTime(2021, 4, 10, 1, 1, 00, 000, DateTimeKind.Local), "Lord Chipmunk", "Scott Jones"));

            modelBuilder.Entity<TourConBooking>().HasData(new TourConBooking().SetTourBooking("C2021041017241", 1, "BOOKED", "Tour", new DateTime(2021, 5, 10, 1, 01, 00, 000, DateTimeKind.Local)
                , 123, "Sentosa Trip", "Scott Jones"));

            modelBuilder.Entity<ReservationInvoice>().HasData(new
            {
                Id = 12,
                GuestId = 1,
                PaymentMethod = "Cash",
                Amount = (decimal)250.0,
                Status = "Paid",
                ReservationId = 1,
                IssueDate = new DateTime(2021, 3, 10, 1, 00, 00, 000, DateTimeKind.Local),
                PaymentType = "ReservationInvoice"
            });

            modelBuilder.Entity<ReservationInvoice>().HasData(new
            {
                Id = 31,
                GuestId = 2,
                PaymentMethod = "Credit Card",
                Amount = (decimal)100.50,
                Status = "Pending",
                ReservationId = 3,
                IssueDate = new DateTime(2021, 4, 4, 1, 00, 00, 000, DateTimeKind.Local),
                PaymentType = "ReservationInvoice"
            });

            modelBuilder.Entity<PostCharge>().HasData(new
            {
                Id = 10,
                GuestId = 3,
                PaymentMethod = "Credit Card",
                Amount = (decimal)48.0,
                Status = "Paid",
                PaymentType = "PostCharge"
            });

            modelBuilder.Entity<PostCharge>().HasData(new
            {
                Id = 15,
                GuestId = 1,
                PaymentMethod = "Cash",
                Amount = (decimal)20.0,
                Status = "Outstanding",
                PaymentType = "PostCharge"
            });

            modelBuilder.Entity<ReceiptItem>().HasData(new
            {
                Id = 102,
                Name = "Laundry",
                Description = "24hrs dry clean and laundry service",
                Quantity = 1,
                Price = (decimal)30.0,
                PostChargeId = 10
            });

            modelBuilder.Entity<ReceiptItem>().HasData(new
            {
                Id = 103,
                Name = "Coke",
                Description = "Coke (bottle) from the minibar fridge",
                Quantity = 3,
                Price = (decimal)6.0,
                PostChargeId = 10
            });

            modelBuilder.Entity<ReceiptItem>().HasData(new
            {
                Id = 204,
                Name = "Broken hairdryer",
                Description = "Hairdryer was found to be broken in the bathroom",
                Quantity = 1,
                Price = (decimal)20.0,
                PostChargeId = 15
            });
        }
    }
}
