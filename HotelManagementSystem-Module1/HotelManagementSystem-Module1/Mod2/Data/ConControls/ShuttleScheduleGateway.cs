using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Data.ConInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Data.Mod2Repository;
using System.Diagnostics;

namespace HotelManagementSystem.Data.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * ShuttleScheduleGateway Class
    */
    public class ShuttleScheduleGateway : IShuttleScheduleDAO
    {
        private readonly Mod2Context _context;
        public ShuttleScheduleGateway(Mod2Context context)
        {
            _context = context;
        }

        public async Task<bool> InsertShuttleBooking(ShuttleSchedule shuttleShedule)
        {
            Debug.WriteLine("DAO - Adding Shuttle Schedule...");
            _context.Add(shuttleShedule);
            Debug.WriteLine("DAO - Added Shuttle Schedule.");
            //Check for successful changes to database
            //Successful
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            //Fail
            else
            {
                return false;
            }
        }

        public List<ShuttleSchedule> RetrieveAllShuttleBooking()
        {
            return _context.ShuttleSchedule.ToList();
        }

        public ShuttleSchedule RetrieveShuttleBookingByID(string id)
        {
            return _context.ShuttleSchedule.AsEnumerable().Where(x => x.RetrieveId() == id).FirstOrDefault();
        }

        public List<ShuttleSchedule> RetrieveAllShuttleBookingByDateTime(DateTime dateTime)
        {

            return _context.ShuttleSchedule.AsEnumerable().Where(x => x.RetrieveScheduleDateTime().Day == dateTime.Day
                && x.RetrieveScheduleDateTime().Month == dateTime.Month && x.RetrieveScheduleDateTime().Year == dateTime.Year
                && x.RetrieveScheduleDateTime().Hour == dateTime.Hour && x.RetrieveScheduleDateTime().Minute == dateTime.Minute).ToList();
        }

        public List<ShuttleSchedule> RetrieveAllShuttleBookingByDirection(string direction)
        {
           return _context.ShuttleSchedule.AsEnumerable().Where(x => x.RetrieveTravelDirection() == direction).ToList();
        }

        public List<ShuttleSchedule> RetrieveAllShuttleBookingByDateAndDirection(DateTime dateTime, string direction)
        {
            Debug.WriteLine("Trying to get all shuttleschedules on date and direction: " + dateTime + direction);

            return _context.ShuttleSchedule.AsEnumerable().Where(x => x.RetrieveScheduleDateTime().Day == dateTime.Day
                && x.RetrieveScheduleDateTime().Month == dateTime.Month && x.RetrieveScheduleDateTime().Year == dateTime.Year
                && x.RetrieveScheduleDateTime().Hour == dateTime.Hour && x.RetrieveScheduleDateTime().Minute == dateTime.Minute
                && x.RetrieveTravelDirection() == direction).ToList();
        }

        public async Task<bool> UpdateShuttleBooking(ShuttleSchedule shuttleShedule)
        {
            _context.Update(shuttleShedule);
            //Check for successful changes to database
            //Successful
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            //Fail
            else
            {
                return false;
            }
        }
    }
}
