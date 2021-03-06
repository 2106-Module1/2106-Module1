﻿using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Data.ConInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Data.Mod2Repository;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Data.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * ShuttlePassengerGateway Class
    */
    public class ShuttlePassengerGateway : IShuttlePassengerDAO
    {
        private readonly Mod2Context _context;
        public ShuttlePassengerGateway(Mod2Context context)
        {
            _context = context;
        }

        public async Task<bool> InsertShuttlePassenger(ShuttlePassenger shuttlePassenger)
        {
            Debug.WriteLine("[DAO] - Adding Shuttle Passenger...");
            _context.Add(shuttlePassenger);
            
            //successful save after add
            if (await _context.SaveChangesAsync() > 0)
            {
                _context.Entry<ShuttlePassenger>(shuttlePassenger).State = EntityState.Detached;    //detaches the object after adding. should solve?
                Debug.WriteLine("[DAO] - Added Shuttle Passenger.");
                return true;
            }

            //Fail
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveShuttlePassengerById(string id)
        {

            // HAS NOT BEEN DEBUG'd yet. 

            Debug.WriteLine("[DAO] - Removing Shuttle Passenger...");

            //EF only allows deletion by 'creating' the to-be-deleted object from the database, then actually deleting it

            ShuttlePassenger passenger = _context.ShuttlePassenger.Local.First(x => x.RetrieveId() == id);

            _context.Remove(passenger);

            //to avoid InvalidOperationException. uncomment below chunk if that exception happens. this 'should' fix it.

            /*if (passenger == null)  
            {
                passenger = new ShuttlePassenger() { Id = id };
                _context.ShuttlePassenger.Attach(passenger);
            }*/

            _context.ShuttlePassenger.Remove(passenger);
            Debug.WriteLine("[DAO] - Removing Shuttle Passenger... Successful.");

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

        public List<ShuttlePassenger> RetrievePassengersOfScheduleIdAndBusId(string shuttleId, string busId)
        {
            return _context.ShuttlePassenger.AsEnumerable().Where(x => x.RetrieveShuttleScheduleId() == shuttleId
            && x.RetrieveShuttleBusId() == busId).ToList();
        }

        public List<ShuttlePassenger> RetrievePassengersOfScheduleId(string scheduleId)
        {
            return _context.ShuttlePassenger.AsEnumerable().Where(x => x.RetrieveShuttleScheduleId() == scheduleId).ToList();
        }

        public List<ShuttlePassenger> RetrievePassengersOfBusId(string busId)
        {
            return _context.ShuttlePassenger.AsEnumerable().Where(x => x.RetrieveShuttleBusId() == busId).ToList();
        }

        public List<string> RetrieveBusesInSameSchedule(string scheduleId)
        {
            // look at passengers whose schedule Id matches
            // look only at their shuttle bus id
            // look only at unique bus IDs
            return _context.ShuttlePassenger.AsEnumerable().Where(x => x.RetrieveShuttleScheduleId() == scheduleId).Select(x => x.RetrieveShuttleBusId()).Distinct().ToList();
        }

        public List<string> RetrieveAllBookedBuses(DateTime dateTime)
        {
            // look only at their shuttle bus id
            // look only at unique bus IDs
            // look only at date time that matches
            return _context.ShuttlePassenger.AsEnumerable().Where(x => x.RetrieveTransactionDateTime().Day == dateTime.Day
                && x.RetrieveTransactionDateTime().Month == dateTime.Month && x.RetrieveTransactionDateTime().Year == dateTime.Year
                && x.RetrieveTransactionDateTime().Hour == dateTime.Hour && x.RetrieveTransactionDateTime().Minute == dateTime.Minute)
                .Select(x => x.RetrieveShuttleBusId())
                .Distinct().ToList();
        }





    }
}
