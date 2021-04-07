using HotelManagementSystem.Data;
using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Data.ConInterfaces;
using HotelManagementSystem.Models.RetrieveInterfaces;
using HotelManagementSystem.Models.RetrieveControls;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HotelManagementSystem.Models.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * ShuttleService Class
    */
    public class ShuttleService : IShuttleServices
    {
        private readonly static DAOFactory EntityFrameworkDAOFactory = DAOFactory.GetDAOFactory(DAOFactory.ENTITY);

        private readonly IShuttleBusPassengerServices _shuttleBusPassengerService;

        private readonly IShuttleScheduleDAO _shuttleScheduleDAO = (ShuttleScheduleGateway)EntityFrameworkDAOFactory.GetShuttleScheduleDAO();

        public ShuttleService(IShuttleBusPassengerServices shuttleBusPassengerService, IShuttleScheduleDAO shuttleScheduleDAO)
        {
            _shuttleBusPassengerService = shuttleBusPassengerService;
            _shuttleScheduleDAO = shuttleScheduleDAO;
        }

        public string GenerateID(int guestID, string direction)
        {
            string GenID = long.Parse(DateTime.Now.ToString("yyyyMMddHHmm")).ToString();

            if (direction.Equals("Arrival"))
            {
                // Shuttle NowDateTimeString GuestID + A for Arrival
                return "S" + GenID + guestID.ToString() + "A";
            }
            else
            {
                // Shuttle NowDateTimeString GuestID + D for Departure
                return "S" + GenID + guestID.ToString() + "D";
            }
        }

        public async Task<bool> AddGuestShuttleBooking(ShuttleSchedule shuttleSchedule)
        {

            Debug.WriteLine("[CREATE] - Adding shuttle schedule of id: " + shuttleSchedule.RetrieveId());

            if (await _shuttleScheduleDAO.InsertShuttleBooking(shuttleSchedule))
            {

                //added shuttle schedule. now call bus/passenger service to add passengers under appropriate buses

                ShuttleSchedule.ReadOnly shuttle = shuttleSchedule.RetrieveShuttleScheduleObject();
                
                if (await _shuttleBusPassengerService.InsertShuttlePassengers(shuttle.Id,
                    shuttle.ScheduleDateTime, shuttle.TravelDirection, shuttle.NumberOfPassengers))
                {

                    //passenger adding is successful
                    return true;
                }


            }

            return false;

        }

        public bool CheckAvailabilityForDateAndTime(DateTime dateTime, string direction, int numOfPassengers)
        {

            Debug.WriteLine("[CHECK] - Checking to add DATETIME: " + dateTime);
            Debug.WriteLine("[CHECK] - Checking to add DIRECTION: " + direction);
            Debug.WriteLine("[CHECK] - Checking to add " + numOfPassengers + " passenger(s).");

            //combining list of active schedules + list of schedules with matching dateTime and direction
            List<ShuttleSchedule> shuttleScheduleList = _shuttleScheduleDAO.RetrieveAllShuttleBookingByDateAndDirectionAndState(dateTime, direction, "CREATED");

            return numOfPassengers == _shuttleBusPassengerService.GetBusSeatsAvailableForShuttleTiming(shuttleScheduleList, dateTime, direction, numOfPassengers);
            
        }

        public async Task<bool> UpdateScheduleStateCancelled(ShuttleSchedule.ReadOnly shuttleSchedule)
        {

            ShuttleSchedule schedule = _shuttleScheduleDAO.RetrieveShuttleBookingByID(shuttleSchedule.Id);
            schedule.SetScheduleState("CANCELLED");

            return (await _shuttleScheduleDAO.UpdateShuttleBooking(schedule));

        }

        public List<ShuttleSchedule> GetAllShuttleSchedules()
        {
            return _shuttleScheduleDAO.RetrieveAllShuttleBooking();
        }

        public ShuttleSchedule GetShuttleScheduleById(string shuttleScheduleId)
        {
            return _shuttleScheduleDAO.RetrieveShuttleBookingByID(shuttleScheduleId);
        }

    }
}
