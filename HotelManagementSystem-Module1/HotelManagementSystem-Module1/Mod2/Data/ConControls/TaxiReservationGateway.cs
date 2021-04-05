using HotelManagementSystem.Data.ConInterfaces;
using HotelManagementSystem.Data.Mod2Repository;
using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConControls
{
    /*
     * Author: Mod 2 Team 2
     * TaxiReservationGateway Class
     */
    public class TaxiReservationGateway : ITaxiReservationDAO
    {
        private readonly Mod2Context _context;

        public TaxiReservationGateway(Mod2Context context)
        {
            _context = context;
        }

        public async Task<bool> InsertTaxiConRes(TaxiConBooking taxiConBooking)
        {
            _context.Add(taxiConBooking);
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

        public List<TaxiConBooking> RetrieveAllTaxi()
        {
            return _context.TaxiConBooking.ToList();
        }

        public TaxiConBooking RetrieveTaxiBookingByConID(string ConBookingID)
        {
            return _context.TaxiConBooking.Find(ConBookingID);
        }

        public async Task<bool> UpdateConResStatus()
        {
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
