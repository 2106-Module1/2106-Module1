using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * ShuttleScheduleAdapter Class
    */
    public class ShuttleScheduleAdapter : IShuttleAdapter
    {
        private readonly ShuttleSchedule _ShuttleSchedule = new ShuttleSchedule();
        public ShuttleScheduleAdapter() { }

        public ShuttleScheduleAdapter(Dictionary<string, object> dict)
        {
            string Id = (string)dict["id"];
            DateTime dateTime = (DateTime)dict["ScheduleDateTime"];
            string Direction = (string)dict["TravelDirection"];
            int GuestId = (int)dict["GuestId"];
            int NumPassenger = (int)dict["NumberOfPassengers"];
            string GuestName = (string)dict["GuestName"];

            _ShuttleSchedule.SetShuttleSchedule(Id, dateTime, Direction, GuestId, NumPassenger, GuestName, "CREATED");
        }
        public ShuttleSchedule GetShuttleSchedule()
        {
            return _ShuttleSchedule;
        }
    }
}
