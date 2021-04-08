using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConEntities
{
    /*
    * Author: Mod 2 Team 2
    * ShuttleSchedule Entity Class
    */
    public class ShuttleSchedule
    {
        [Key]
        private string Id { get; set; }
        private DateTime ScheduleDateTime { get; set; }
        private string TravelDirection { get; set; }
        private int GuestId { get; set; }
        private int NumberOfPassengers { get; set; }
        private DateTime TransactionDateTime { get; set; }
        private string GuestName { get; set; }

        public ShuttleSchedule() { }

        public ShuttleSchedule(string id, DateTime dateTime, string direction, int guestId, int numPassenger, string guestName)
        {
            Id = id;
            ScheduleDateTime = dateTime;
            TravelDirection = direction;
            GuestId = guestId;
            GuestName = guestName;
            NumberOfPassengers = numPassenger;
            TransactionDateTime = DateTime.Now;
        }

        public void SetShuttleSchedule(string scheduleId, DateTime dateTime, string direction, int guestId, int numPassenger, string guestName)
        {
            Id = scheduleId;
            ScheduleDateTime = dateTime;
            TravelDirection = direction;
            GuestId = guestId;
            NumberOfPassengers = numPassenger;
            GuestName = guestName;
            TransactionDateTime = DateTime.Now;
        }

        public string RetrieveId()
        {
            return Id;
        }
        public DateTime RetrieveScheduleDateTime()
        {
            return ScheduleDateTime;
        }

        public string RetrieveTravelDirection()
        {
            return TravelDirection;
        }

        public class ReadOnly
        {
            public string Id { get; set; }
            public DateTime ScheduleDateTime { get; set; }
            public string TravelDirection { get; set; }
            public int GuestId { get; set; }
            public int NumberOfPassengers { get; set; }
            public DateTime TransactionDateTime { get; set; }
            public string GuestName { get; set; }
        }

        public ReadOnly RetrieveShuttleScheduleObject()
        {
            ReadOnly returnObject = new ReadOnly
            {
                Id = Id,
                ScheduleDateTime = ScheduleDateTime,
                TravelDirection = TravelDirection,
                GuestId = GuestId,
                NumberOfPassengers = NumberOfPassengers,
                TransactionDateTime = TransactionDateTime,
                GuestName = GuestName
            };

            return returnObject;
        }
    }
}
