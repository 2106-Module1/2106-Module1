using HotelManagementSystem.Data;
using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Data.ConInterfaces;
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

        private readonly IShuttleScheduleDAO _shuttleScheduleDAO = (ShuttleScheduleGateway)EntityFrameworkDAOFactory.GetShuttleScheduleDAO();
        private readonly IShuttleBusDAO _shuttleBusDAO = (ShuttleBusGateway)EntityFrameworkDAOFactory.GetShuttleBusDAO();   
        private readonly IShuttlePassengerDAO _shuttlePassengerDAO = (ShuttlePassengerGateway)EntityFrameworkDAOFactory.GetShuttlePassengerDAO();

        public ShuttleService(IShuttleScheduleDAO shuttleScheduleDAO, IShuttleBusDAO shuttleBusDAO, IShuttlePassengerDAO shuttlePassengerDAO)
        {
            _shuttleScheduleDAO = shuttleScheduleDAO;
            _shuttleBusDAO = shuttleBusDAO;
            _shuttlePassengerDAO = shuttlePassengerDAO;
        }

        public string GenerateID(DateTime datetime, int guestID)
        {
            string GenID = long.Parse(datetime.ToString("yyyyMMddHHmm")).ToString();

            // Shuttle DateTimeString GuestID
            return "S" + GenID + guestID.ToString();
        }

        public async Task<bool> AddGuestShuttleBooking(ShuttleSchedule.ReadOnly shuttleSchedule)
        {

            //shuttleSchedule attrbute values. declared here for easier typing.
            DateTime dateTime = shuttleSchedule.ScheduleDateTime;
            int numOfPassengers = shuttleSchedule.NumberOfPassengers;
            string direction = shuttleSchedule.TravelDirection;
            int guestId = shuttleSchedule.GuestId;
            string guestName = shuttleSchedule.GuestName;

            int passengerCounter = 0;   //to keep track of passenger Id when passengers are split across several buses

            Debug.WriteLine("[CREATE] - Trying to add shuttleschedule...");

            if (CheckAvailabilityForDateAndTime(dateTime, direction, numOfPassengers).Result)
            {
                //seats are good. because seats are good, adding 

                //follows roughly the same logic as CheckAvailability, but with actual additions.
                //actually, considering CheckAvai to return a string instead. depending on the string, this method will add in an appropriate manner.
                //^ less(?) code but may be convoluted. 

                //[CREATE]the ShuttleSchedule object first
                ShuttleSchedule schedule = new ShuttleSchedule(GenerateID(dateTime, guestId), dateTime, direction, guestId, numOfPassengers, guestName);

                //add it into the database, since we already know it is 'legal'.
                await _shuttleScheduleDAO.InsertShuttleBooking(schedule);

                List<ShuttleSchedule> shuttleScheduleList = _shuttleScheduleDAO.RetrieveAllShuttleBookingByDateAndDirection(dateTime, direction);

                // 2. try to add seats into currently booked buses.

                List<ShuttlePassenger> passengerList;
                ShuttleBus bus;
                ShuttlePassenger newPassenger;

                int neededAmountOfSeats = numOfPassengers;

                Debug.WriteLine("[CREATE] - Looking to add " + neededAmountOfSeats + " passengers ");

                //for every shuttleschedule in the same direction
                foreach (ShuttleSchedule s in shuttleScheduleList)
                {
                    List<string> busIdList = _shuttlePassengerDAO.RetrieveBusesInSameSchedule(s.RetrieveId()); //look at a new list of buses

                    //for every bus related to this schedule
                    foreach (string busId in busIdList)
                    {
                        bus = _shuttleBusDAO.RetrieveShuttleBusById(busId);

                        passengerList = _shuttlePassengerDAO.RetrievePassengersOfBusId(bus.RetrieveId());


                        if (passengerList.Count == bus.RetrieveShuttleBusCapacity())
                        {
                            //this bus is full. go to next available bus.
                            Debug.WriteLine("[CREATE] - Bus of Id " + bus.RetrieveId() + " already fully booked. Moving to next.");
                            continue;
                        }

                        if (passengerList.Count + neededAmountOfSeats <= bus.RetrieveShuttleBusCapacity())
                        {
                            //possible to book all remaining needed seats under this bus.
                            Debug.WriteLine("[CREATE] - Creating " + neededAmountOfSeats + " amount of ShuttlePassengers for ShuttleBus " + bus.RetrieveId());

                            for (int i=0; i<neededAmountOfSeats; i++)
                            {
                                //[CREATE]a new ShuttlePassenger
                                newPassenger = new ShuttlePassenger(schedule.RetrieveId() + i, DateTime.Now,
                                    schedule.RetrieveId(), bus.RetrieveId(), i.ToString());

                                await _shuttlePassengerDAO.InsertShuttlePassenger(newPassenger);
                                Debug.WriteLine("[CREATE] - added passenger of Id " + newPassenger.RetrieveId());
                            }

                            Debug.WriteLine("[CREATE] - all passengers accounted for. End creation.");

                            return true;
                        }

                        else
                        {
                            //current bus can take 'some' passengers, but not all.
                            //update number of seats needed. minus from amount of seats current bus can offer.


                            Debug.WriteLine("[CREATE] - Not all seats can be created for bus id " + bus.RetrieveId());

                            //get number of seats that we can put in 'right now'

                            int possibleSeatsCount = bus.RetrieveShuttleBusCapacity() - passengerList.Count;

                            Debug.WriteLine("[CREATE] - Creating " + possibleSeatsCount + " amount of ShuttlePassengers for ShuttleBus " + bus.RetrieveId());

                            for (int i = 0; i < possibleSeatsCount; i++)
                            {
                                //[CREATE]a new ShuttlePassenger
                                newPassenger = new ShuttlePassenger(schedule.RetrieveId() + passengerCounter, DateTime.Now,
                                    schedule.RetrieveId(), bus.RetrieveId(), passengerCounter.ToString());

                                await _shuttlePassengerDAO.InsertShuttlePassenger(newPassenger);
                                Debug.WriteLine("[CREATE] - added passenger of Id " + newPassenger.RetrieveId());

                                passengerCounter++;
                            }

                            neededAmountOfSeats -= possibleSeatsCount;  //update remaining amt of seats to add

                            Debug.WriteLine("[CREATE] - " + neededAmountOfSeats + " more seats needed.");
                        }

                    }

                }

                Debug.WriteLine("[CREATE] - All currently-booked buses are full. Checking unbooked buses for " + neededAmountOfSeats + " seats.");

                List<string> bookedBusList = _shuttlePassengerDAO.RetrieveAllBookedBuses();

                List<ShuttleBus> busList = _shuttleBusDAO.RetrieveAllShuttleBuses();

                bool isBooked;

                //for every bus in the system
                foreach (ShuttleBus b in busList)
                {
                    isBooked = false;

                    //check if bus in system is referenced by currently booked buses
                    foreach (string id in bookedBusList)
                    {
                        if (b.RetrieveId() == id)
                        {
                            //id matches. bus 'b' is already booked.
                            //end this current foreloop so we can proceed to next bus.
                            Debug.WriteLine("[CREATE] - Bus of Id " + id + " already fully booked. Moving to next.");
                            isBooked = true;
                            break;
                        }
                    }

                    if (isBooked)
                        continue;   //bus is booked, we are only looking for unbooked buses. go to next bus in database.
                    else
                    {
                        //this bus isn't booked. can try to add slots into it.
                        if (neededAmountOfSeats <= b.RetrieveShuttleBusCapacity())
                        {
                            Debug.WriteLine("[CREATE] - Booking " + neededAmountOfSeats + " amount of seats for empty bus " + b.RetrieveId());

                            for (int i = 0; i < neededAmountOfSeats; i++)
                            {
                                //[CREATE]a new ShuttlePassenger
                                newPassenger = new ShuttlePassenger(schedule.RetrieveId() + passengerCounter, DateTime.Now,
                                    schedule.RetrieveId(), b.RetrieveId(), passengerCounter.ToString());

                                await _shuttlePassengerDAO.InsertShuttlePassenger(newPassenger);
                                Debug.WriteLine("[CREATE] - added passenger of Id " + newPassenger.RetrieveId());

                                passengerCounter++;
                            }

                            Debug.WriteLine("[CREATE] - all passengers accounted for. End creation.");
                            return true;        
                        }

                        else
                        {
                            Debug.WriteLine("[CREATE] - OVER-CAPACITY - Unable to book " + neededAmountOfSeats + " amount of seat(s) for empty bus " + b.RetrieveId());

                            //can't book all at one go. book the bus fully, then go to the next bus.

                            for (int i = 0; i < b.RetrieveShuttleBusCapacity(); i++)
                            {
                                //[CREATE]a new ShuttlePassenger
                                newPassenger = new ShuttlePassenger(schedule.RetrieveId() + passengerCounter, DateTime.Now,
                                    schedule.RetrieveId(), b.RetrieveId(), passengerCounter.ToString());

                                await _shuttlePassengerDAO.InsertShuttlePassenger(newPassenger);
                                Debug.WriteLine("[CREATE] - added passenger of Id " + newPassenger.RetrieveId() + " under bus of Id " + newPassenger.RetrieveShuttleBusId());

                                passengerCounter++;
                            }

                            neededAmountOfSeats -= b.RetrieveShuttleBusCapacity();
                            Debug.WriteLine("[CREATE] - " + neededAmountOfSeats + " more seat(s) needed.");
                            //update required amount of seats. then run the loop again.
                        }
                    }

                }


                //end of all loops. if we reached here, it means that neededAmountOfSeats isn't 0.
                //which means that something has went very wrong, may be database issues.
                Debug.WriteLine("[CREATE/fail] - Issue while creating shuttle passengers. Something is very wrong with ShuttleService here.");
                Debug.WriteLine("[CREATE/fail] - neededAmountOfSeats count is: " + neededAmountOfSeats);
                return false;
            }

            else
            {
                //checkAvailability check has failed
                Debug.WriteLine("[CREATE/fail] - Unable to create shuttle. There is a conflict with existing shuttle schedules.");
                return false;
            }
        }

        public async Task<bool> CheckAvailabilityForDateAndTime(DateTime dateTime, string direction, int numOfPassengers)
        {


            // 1. Retrieve all schedules for specified datatime and direction

            //22-03 ISSUE HERE - "CANT TRANSLATE LINQ EXPRESSION TO SQL BLAH BLAH"
            List<ShuttleSchedule> shuttleScheduleList = _shuttleScheduleDAO.RetrieveAllShuttleBookingByDateAndDirection(dateTime, direction);

            // 2. look at all buses related to each shuttle schedule. see if seats can be placed within them.

            List<ShuttlePassenger> passengerList;
            ShuttleBus bus;

            int neededAmountOfSeats = numOfPassengers;

            Debug.WriteLine("[CHECK] - Checking if current schedules can fit " + neededAmountOfSeats + " passenger(s).");

            //for every shuttleschedule in the same direction
            foreach (ShuttleSchedule s in shuttleScheduleList)
            {

                List<string> busIdList = _shuttlePassengerDAO.RetrieveBusesInSameSchedule(s.RetrieveId()); //look at a new list of buses

                //for every bus related to this schedule
                foreach (string busId in busIdList)
                {
                    bus = _shuttleBusDAO.RetrieveShuttleBusById(busId);

                    passengerList = _shuttlePassengerDAO.RetrievePassengersOfBusId(bus.RetrieveId());

                    if (passengerList.Count == bus.RetrieveShuttleBusCapacity())
                    {
                        //this bus is full. go to next available bus.
                        Debug.WriteLine("[CHECK] - Bus of Id " + bus.RetrieveId() + " already fully booked.");
                        continue;  
                    }

                    //(current number of passengers) + (to-be-added passengers) is less or equal to (max capacity)

                    if (passengerList.Count + neededAmountOfSeats <= bus.RetrieveShuttleBusCapacity())
                    {
                        //possible to book all remaining needed seats under this bus.
                        Debug.WriteLine("[CHECK/pass] - GOOD CAPACITY - Able to book " + neededAmountOfSeats + " amount of seats for bus " + bus.RetrieveId());

                        return true;
                    }

                    else
                    {
                        //current bus can take 'some' passengers, but not all.
                        //update number of seats needed. minus from amount of seats current bus can offer.
                        Debug.WriteLine("[CHECK] - OVER CAPACITY - Unable to book " + neededAmountOfSeats + " amount of seats for empty bus " + bus.RetrieveId());


                        neededAmountOfSeats -= (bus.RetrieveShuttleBusCapacity() - passengerList.Count);

                        Debug.WriteLine("[CHECK] - " + neededAmountOfSeats + " more seats needed.");
                    }

                }

            }

            Debug.WriteLine("[CHECK] - All currently booked buses are full. Checking unbooked buses for " + neededAmountOfSeats + " seats.");

            List<string> bookedBusList = _shuttlePassengerDAO.RetrieveAllBookedBuses();

            List<ShuttleBus> busList = _shuttleBusDAO.RetrieveAllShuttleBuses();

            bool isBooked;

            //for every bus in the system
            foreach (ShuttleBus b in busList)
            {
                isBooked = false;

                //check if bus in system is referenced by currently booked buses
                foreach (string id in bookedBusList)
                {
                    if (b.RetrieveId() == id)
                    {
                        //id matches. bus 'b' is already booked.
                        //end this current foreloop so we can proceed to next bus.
                        Debug.WriteLine("[CHECK] - Bus of Id " + id + " already booked.");
                        isBooked = true;
                        break;
                    }
                }

                if (isBooked)
                    continue;   //bus is booked, we are only looking for unbooked buses. go to next bus in database.
                else
                {
                    //this bus isn't booked. can try to add slots into it.
                    if (neededAmountOfSeats <= b.RetrieveShuttleBusCapacity())
                    {
                        Debug.WriteLine("[CHECK/pass] - GOOD CAPACITY - Able to book " + neededAmountOfSeats + " amount of seats for empty bus " + b.RetrieveId());
                        return true;        //bus have enough seats. check passed.
                    }
                        
                    else
                    {
                        Debug.WriteLine("[CHECK] - OVER-CAPACITY - Unable to book " + neededAmountOfSeats + " amount of seats for empty bus " + b.RetrieveId());
                        neededAmountOfSeats -= b.RetrieveShuttleBusCapacity();
                        Debug.WriteLine("[CHECK] - " + neededAmountOfSeats + " more seats needed.");
                        //update required amount of seats. then run the loop again.
                    }
                }

            }

            // if all checks are failed, means that there are no seats available.
            Debug.WriteLine("[CHECK/fail] - NO SEATS AVAILABLE. Check finished");
            return false;
        }


        public List<ShuttleSchedule> GetAllShuttleSchedules()
        {
            return _shuttleScheduleDAO.RetrieveAllShuttleBooking();
        }
        public List<ShuttlePassenger> GetShuttlePassengersOfShuttleSchedule(string shuttleScheduleId)
        {
            return _shuttlePassengerDAO.RetrievePassengersOfScheduleId(shuttleScheduleId);
        }

        public ShuttleSchedule GetShuttleScheduleById(string shuttleScheduleId)
        {
            return _shuttleScheduleDAO.RetrieveShuttleBookingByID(shuttleScheduleId);
        }
    }
}
