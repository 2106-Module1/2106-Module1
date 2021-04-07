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
    public class ShuttleBusPassengerService : IShuttleBusPassengerServices
    {
        private readonly static DAOFactory EntityFrameworkDAOFactory = DAOFactory.GetDAOFactory(DAOFactory.ENTITY);

        private readonly IShuttleScheduleDAO _shuttleScheduleDAO = (ShuttleScheduleGateway)EntityFrameworkDAOFactory.GetShuttleScheduleDAO();
        private readonly IShuttleBusDAO _shuttleBusDAO = (ShuttleBusGateway)EntityFrameworkDAOFactory.GetShuttleBusDAO();   
        private readonly IShuttlePassengerDAO _shuttlePassengerDAO = (ShuttlePassengerGateway)EntityFrameworkDAOFactory.GetShuttlePassengerDAO();

        public ShuttleBusPassengerService(IShuttleScheduleDAO shuttleScheduleDAO, IShuttleBusDAO shuttleBusDAO, IShuttlePassengerDAO shuttlePassengerDAO)
        {
            _shuttleScheduleDAO = shuttleScheduleDAO;
            _shuttleBusDAO = shuttleBusDAO;
            _shuttlePassengerDAO = shuttlePassengerDAO;
        }

        public string GeneratePassengerID(string scheduleID, string passengerIndex)
        {
            return scheduleID + "SP" + passengerIndex.ToString();
        }

        public async Task<bool> InsertShuttlePassengers(string scheduleId, DateTime dateTime, string direction, int numOfPassengers)
        {
            List<ShuttlePassenger> passengersToAdd = PrepareShuttlePassengers(scheduleId, dateTime, direction, numOfPassengers);

            if (passengersToAdd == null)   
            {
                Debug.WriteLine("[InsertShuttlePassengers - CREATE/fail] - Unable to support adding of " + numOfPassengers + " passengers.");
                return false;
            }  

            foreach (ShuttlePassenger p in passengersToAdd)
            {
                bool isAddSuccessfull = await _shuttlePassengerDAO.InsertShuttlePassenger(p);

                if (!isAddSuccessfull)
                {
                    //some database issue when adding a new passenger. 
                    Debug.WriteLine("[InsertShuttlePassengers - CREATE] - Passenger adding failed. Ending add operation.");
                    return false;
                }

                Debug.WriteLine("[InsertShuttlePassengers - CREATE] - Successfully added passenger of ID " + p.RetrieveId());
            }
            
            return true;

        }

        //helper function.
        //runs through every shuttleschedule, passenger and bus to determine the most efficient way to add seats
        //compiles and returns a List<ShuttlePassenger> object that represents the passengers to add
        public List<ShuttlePassenger> PrepareShuttlePassengers(string scheduleId, DateTime dateTime, string direction, int numOfPassengers)
        {

            List<ShuttlePassenger> passengerList;
            ShuttleBus bus;
            ShuttlePassenger newPassenger;

            List<ShuttlePassenger> passengersToAdd = new List<ShuttlePassenger>();

            int passengerCounter = 1;   //to keep track of passenger Id when passengers are split across several buses

            int neededAmountOfSeats = numOfPassengers;

            List<ShuttleSchedule> shuttleScheduleList = _shuttleScheduleDAO.RetrieveAllShuttleBookingByDateAndDirectionAndState(dateTime, direction, "CREATED");
            List<string> busIdList = new List<string>();

            List<string> accessedBuses = new List<string>();

            //for every shuttleschedule in the same direction and time
            //we need the buses under these schedules
            foreach (ShuttleSchedule s in shuttleScheduleList)
            {
                Debug.WriteLine("[PrepareShuttlePassengers] - Checking schedule of id: " + s.RetrieveId());

                if (busIdList == null)
                {
                    busIdList = GetBusIdInSameSchedule(s.RetrieveId());
                }
                else
                {
                    busIdList.AddRange(GetBusIdInSameSchedule(s.RetrieveId()));
                    List<string> tempList = busIdList;
                    busIdList = tempList.Distinct().ToList();  //get only distinct new buses. ensures no duplicate buses.


                    //shuttle bus 1 has duplicates in this - why?

                }

                foreach (string busId in busIdList)
                {
                    //quick check to see if busId has been interacted with before
                    bool hasBeenAccessed = false;
                    foreach (string accessedBusId in accessedBuses)
                    {
                        if (busId == accessedBusId)
                            hasBeenAccessed = true;
                    }

                    if (hasBeenAccessed)
                        continue;

                    accessedBuses.Add(busId);

                    //end of quick check

                    Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - CHECK] - Looking at Bus " + busId);

                    bus = _shuttleBusDAO.RetrieveShuttleBusById(busId);

                    passengerList = GetShuttlePassengersOfBus(busId, dateTime, direction);
                    Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - CHECK] - Bus has " + passengerList.Count + " passengers");

                    if (passengerList.Count == bus.RetrieveShuttleBusCapacity())
                    {
                        //this bus is full. go to next available bus.
                        Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - CHECK] - Bus of Id " + bus.RetrieveId() + " already fully booked.");
                        continue;
                    }

                    if (passengerList.Count + neededAmountOfSeats <= bus.RetrieveShuttleBusCapacity())
                    {
                        //possible to book all remaining needed seats under this bus.
                        Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - CHECK/pass] - GOOD CAPACITY - Able to book " + neededAmountOfSeats +
                            " amount of seats for bus " + bus.RetrieveId());
                        
                        for (int i = 0; i < neededAmountOfSeats; i++)
                        {
                            newPassenger = new ShuttlePassenger(GeneratePassengerID(scheduleId, passengerCounter.ToString()), DateTime.Now,
                               scheduleId, bus.RetrieveId(), passengerCounter.ToString());

                            Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - ADD] - Preparing passenger of Id " + newPassenger.RetrieveId());

                            passengersToAdd.Add(newPassenger);

                            passengerCounter++;
                        }

                        return passengersToAdd;
                    }

                    else
                    {
                        //can add some seats, but not enough to finish the booking

                        int possibleSeatsCount = bus.RetrieveShuttleBusCapacity() - passengerList.Count;

                        Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - CHECK] - OVER CAPACITY - " +
                            "Unable to book " + neededAmountOfSeats + " amount of seats for bus " + bus.RetrieveId());
                        Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - CHECK] - OVER CAPACITY - " +
                            "Bus " + bus.RetrieveId() + " currently can offer " +
                            (possibleSeatsCount) + " seats.");

                        neededAmountOfSeats -= possibleSeatsCount;  //update remaining amt of seats to add

                        for (int i = 0; i < possibleSeatsCount; i++)
                        {
                            newPassenger = new ShuttlePassenger(GeneratePassengerID(scheduleId, passengerCounter.ToString()), DateTime.Now,
                               scheduleId, bus.RetrieveId(), passengerCounter.ToString());

                            Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - ADD] - Preparing passenger of Id " + newPassenger.RetrieveId());

                            passengersToAdd.Add(newPassenger);

                            passengerCounter++;
                        }

                        Debug.WriteLine("[PrepareShuttlePassengers - EXISTING - CHECK] - " + neededAmountOfSeats + " more seats needed.");

                    }

                    //proceed to next available shuttle schedule
                    //^SHOULD NOT be the same shuttleschedule

                }

            }

            Debug.WriteLine("[PrepareShuttlePassengers] - All currently booked buses are full. " +
                "Checking unbooked buses for " + neededAmountOfSeats + " seats.");

            List<string> bookedBusList = busIdList; //same list as in the first for loop

            List<ShuttleBus> busList = _shuttleBusDAO.RetrieveAllShuttleBuses();

            bool isBooked;

            // for every bus in the system
            // we need to look for unbooked buses.
            foreach (ShuttleBus b in busList)
            {
                isBooked = false;

                if (bookedBusList != null)
                {
                    //check if bus in system is referenced by currently booked buses
                    // we don't want to book slots for buses that we already know are full

                    //also, prevents null exception in the later for loop

                    foreach (string id in bookedBusList)
                    {
                        if (b.RetrieveId() == id)
                        {
                            //id matches. bus 'b' is already booked.
                            //end this current foreloop so we can proceed to next bus.
                            Debug.WriteLine("[PrepareShuttlePassengers - UNBOOKED - CHECK] - Bus of Id " + id + " already booked.");
                            isBooked = true;
                            break;
                        }
                    }
                }

                if (isBooked)
                    continue;   //bus is booked, we are only looking for unbooked buses. go to next bus in database.
                else
                {
                    //this bus isn't booked. can try to add passengers into it
                    if (neededAmountOfSeats <= b.RetrieveShuttleBusCapacity())
                    {
                        Debug.WriteLine("[PrepareShuttlePassengers - UNBOOKED - CHECK/pass] - GOOD CAPACITY - Able to book " + neededAmountOfSeats + " amount of seats for empty bus " + b.RetrieveId());

                        for (int i = 0; i < neededAmountOfSeats; i++)
                        {
                            newPassenger = new ShuttlePassenger(GeneratePassengerID(scheduleId, passengerCounter.ToString()), DateTime.Now,
                               scheduleId, b.RetrieveId(), passengerCounter.ToString());

                            Debug.WriteLine("[PrepareShuttlePassengers - UNBOOKED - ADD] - Preparing passenger of Id " + newPassenger.RetrieveId());

                            passengersToAdd.Add(newPassenger);

                            passengerCounter++;
                        }

                        return passengersToAdd;        //bus have enough seats. check passed.
                    }

                    else
                    {
                        Debug.WriteLine("[PrepareShuttlePassengers - UNBOOKED - CHECK] - OVER-CAPACITY - Unable to book " + neededAmountOfSeats + " amount of seats for empty bus " + b.RetrieveId());

                        //can't book all at one go. book the bus until its full, then go to the next bus.

                        int possibleSeatsCount = b.RetrieveShuttleBusCapacity();

                        Debug.WriteLine("[PrepareShuttlePassengers - UNBOOKED - CHECK] - OVER CAPACITY - " +
                            "Empty Bus " + b.RetrieveId() + " can offer " +
                            (possibleSeatsCount) + " seats.");


                        for (int i = 0; i < possibleSeatsCount; i++)
                        {
                            newPassenger = new ShuttlePassenger(GeneratePassengerID(scheduleId, passengerCounter.ToString()), DateTime.Now,
                               scheduleId, b.RetrieveId(), passengerCounter.ToString());

                            Debug.WriteLine("[PrepareShuttlePassengers - UNBOOKED - ADD] - Preparing passenger of Id " + newPassenger.RetrieveId());

                            passengersToAdd.Add(newPassenger);

                            passengerCounter++;
                        }

                        neededAmountOfSeats -= possibleSeatsCount;
                        Debug.WriteLine("[PrepareShuttlePassengers - UNBOOKED - CHECK] - " + neededAmountOfSeats + " more seats needed.");
                        //update required amount of seats. then run the loop again.
                    }
                }

            }

            Debug.WriteLine("[PrepareShuttlePassengers - CHECK/FAIL] - Not enough seats in the system. Failed check.");

            return null;
        }

        public int GetBusSeatsAvailableForShuttleTiming(List<ShuttleSchedule> shuttleScheduleList, DateTime dateTime, string direction, int neededAmountOfSeats)
        {
            List<string> busIdList = null;
            int totalRequiredSeats = neededAmountOfSeats;

            foreach (ShuttleSchedule s in shuttleScheduleList)
            {

                //get a list of bus that are used by these existing shuttle schedules

                if (busIdList == null)
                {
                    busIdList = GetBusIdInSameSchedule(s.RetrieveId());
                }
                else
                {
                    busIdList.AddRange(GetBusIdInSameSchedule(s.RetrieveId()));
                    List<string> tempList = busIdList;
                    busIdList = tempList.Distinct().ToList();  //get only distinct new buses. ensures no duplicate buses.

                }

                //for every bus related to this schedule
                foreach (string busId in busIdList)
                {
                    //update needed seats accordingly
                    neededAmountOfSeats -= GetAvailableSeatCountFromBookedBus(dateTime, direction, busId, neededAmountOfSeats);

                    if (neededAmountOfSeats <= 0)
                        return totalRequiredSeats;

                    Debug.WriteLine("[CHECK] - Need " + neededAmountOfSeats + " more seats.");
                }

            }

            Debug.WriteLine("[GetBusSeatsAvailableForShuttleTiming - CHECK] - All currently booked buses are full." +
                "Checking unbooked buses for " + neededAmountOfSeats + " seats.");

            int finalPossibleSeats = GetBusSeatsAvailableFromUnbookedBuses(busIdList, neededAmountOfSeats);

            if (finalPossibleSeats >= neededAmountOfSeats)
                return totalRequiredSeats;


            // if all checks are failed, means that there are no seats available.
            Debug.WriteLine("[GetBusSeatsAvailableForShuttleTiming - CHECK/fail] - NOT ENOUGH SEATS AVAILABLE. Check finished");
            Debug.WriteLine("[CHECK/fail] - Unable to add " + totalRequiredSeats + " passenger(s).");
            Debug.WriteLine("[CHECK/fail] - System can only take " + finalPossibleSeats + " more passenger(s).");
            return finalPossibleSeats;

            
        }

        // helper function to get amount of available seats from a booked bus
        public int GetAvailableSeatCountFromBookedBus(DateTime dateTime, string direction, string busId, int neededAmountOfSeats)
        {
            int requiredSeats = neededAmountOfSeats;

            ShuttleBus bus = _shuttleBusDAO.RetrieveShuttleBusById(busId);

            //find a proper way to get all passengers of a bus
            //currently this only gives you passengers of that schedule
            List<ShuttlePassenger> passengerList = GetShuttlePassengersOfBus(busId, dateTime, direction);

            if (passengerList.Count == bus.RetrieveShuttleBusCapacity())
            {
                //this bus is full. go to next available bus.
                Debug.WriteLine("[GetAvailableSeatCountFromBookedBus - CHECK] - Bus of Id " + bus.RetrieveId() + " already fully booked.");
                return 0;
            }

            //(current number of passengers) + (to-be-added passengers) is less or equal to (max capacity)
            if (passengerList.Count + requiredSeats <= bus.RetrieveShuttleBusCapacity())
            {
                //possible to book all remaining needed seats under this bus.
                Debug.WriteLine("[GetAvailableSeatCountFromBookedBus - CHECK/pass] - GOOD CAPACITY - Able to book " + requiredSeats + " amount of seats for bus " + bus.RetrieveId());

                return requiredSeats;
            }

            else
            {
                //current bus can take 'some' passengers, but not all.

                //get number of seats that we can put in 'right now'

                int possibleSeatsCount = bus.RetrieveShuttleBusCapacity() - passengerList.Count;

                Debug.WriteLine("[GetAvailableSeatCountFromBookedBus - CHECK] - OVER CAPACITY - " +
                    "Unable to book " + requiredSeats + " amount of seats for bus " + bus.RetrieveId());
                Debug.WriteLine("[GetAvailableSeatCountFromBookedBus - CHECK] - OVER CAPACITY - " +
                    "Bus " + bus.RetrieveId() + " currently can offer " +
                    (bus.RetrieveShuttleBusCapacity() - passengerList.Count) + " seats.");

                requiredSeats -= possibleSeatsCount;  //update remaining amt of seats to add

                Debug.WriteLine("[GetAvailableSeatCountFromBookedBus - CHECK] - " + requiredSeats + " more seats needed.");

                return (bus.RetrieveShuttleBusCapacity() - passengerList.Count);
            }

        }

        // helper function to get amount of available seats from unbooked buses
        public int GetBusSeatsAvailableFromUnbookedBuses(List<string> busIdList, int requiredSeats)
        {
            List<string> bookedBusList = busIdList; //same list as in the first for loop

            List<ShuttleBus> busList = _shuttleBusDAO.RetrieveAllShuttleBuses();

            int neededAmountOfSeats = requiredSeats;

            bool isBooked;

            // for every bus in the system
            // we need to look for unbooked buses.
            foreach (ShuttleBus b in busList)
            {
                isBooked = false;

                if (bookedBusList != null)
                {
                    //check if bus in system is referenced by currently booked buses
                    // we don't want to book slots for buses that we already know are full
                    //INCLUDES BUSES THAT WE PREVIOUSLY ADDED.

                    //also, prevents null exception in the later for loop

                    foreach (string id in bookedBusList)
                    {
                        if (b.RetrieveId() == id)
                        {
                            //id matches. bus 'b' is already booked.
                            //end this current foreloop so we can proceed to next bus.
                            Debug.WriteLine("[GetBusSeatsAvailableFromUnbookedBuses - CHECK] - Bus of Id " + id + " already booked.");
                            isBooked = true;
                            break;
                        }
                    }
                }

                if (isBooked)
                    continue;   //bus is booked, we are only looking for unbooked buses. go to next bus in database.
                else
                {
                    //this bus isn't booked. can try to add passengers into it
                    if (neededAmountOfSeats <= b.RetrieveShuttleBusCapacity())
                    {
                        Debug.WriteLine("[GetBusSeatsAvailableFromUnbookedBuses - CHECK/pass] - GOOD CAPACITY - Able to book " + neededAmountOfSeats + " amount of seats for empty bus " + b.RetrieveId());

                        return requiredSeats;        //bus have enough seats. check passed.
                    }

                    else
                    {
                        Debug.WriteLine("[GetBusSeatsAvailableFromUnbookedBuses - CHECK] - OVER-CAPACITY - Unable to book " + neededAmountOfSeats + " amount of seats for empty bus " + b.RetrieveId());

                        //can't book all at one go. book the bus until its full, then go to the next bus.

                        neededAmountOfSeats = neededAmountOfSeats - b.RetrieveShuttleBusCapacity();
                        Debug.WriteLine("[GetBusSeatsAvailableFromUnbookedBuses - CHECK] - " + neededAmountOfSeats + " more seats needed.");
                        //update required amount of seats. then run the loop again.
                    }
                }

            }

            //end-all catch. in case something slips by the earlier return statements

            if (neededAmountOfSeats > 0)
                return 0;   //represents that there's not enough seats
            
            else
                return neededAmountOfSeats;

        }

        public List<ShuttlePassenger> GetShuttlePassengersOfShuttleSchedule(string shuttleScheduleId)
        {
            return _shuttlePassengerDAO.RetrievePassengersOfScheduleId(shuttleScheduleId);
        }

        public List<string> GetBusIdInSameSchedule(string shuttleScheduleId)
        {
            return _shuttlePassengerDAO.RetrieveBusesInSameSchedule(shuttleScheduleId);
        }

        public List<ShuttlePassenger> GetShuttlePassengersOfBus(string busId, DateTime scheduleDateTime, string direction)
        {
            
            List<ShuttlePassenger> busPassengerList = new List<ShuttlePassenger>();
            List<ShuttlePassenger> passengers = null;

            List<ShuttleSchedule> shuttleSchedules = _shuttleScheduleDAO.RetrieveAllShuttleBookingByDateAndDirectionAndState(
                scheduleDateTime, direction, "CREATED");

            foreach (ShuttleSchedule schedule in shuttleSchedules)
            {
                if (passengers == null)
                    passengers = _shuttlePassengerDAO.RetrievePassengersOfScheduleId(schedule.RetrieveId());
                else
                {
                    passengers.AddRange(_shuttlePassengerDAO.RetrievePassengersOfScheduleId(schedule.RetrieveId()));
                    List<ShuttlePassenger> tempList = passengers;
                    passengers = tempList.Distinct().ToList();  //ensures no duplicates
                }
                    
            }

            foreach (ShuttlePassenger p in passengers)
            {

                if (p.RetrieveShuttleBusId() == busId)
                    busPassengerList.Add(p);
                    
            }

            return busPassengerList;

        }


    }
}
