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
     * TourReservationGateway Class
     */
    public class TourReservationGateway : ITourReservationDAO
    {
        private readonly Mod2Context _context;

        public TourReservationGateway(Mod2Context context)
        {
            _context = context;
        }

        public async Task<bool> InsertTourConRes(TourConBooking tourConBooking)
        {
            _context.Add(tourConBooking);
            //Check for successful changes to database
            //Successful
            if (await _context.SaveChangesAsync()>0){
                return true;
            }
            //Fail
            else{
                return false;
            }
        }

        public TourConBooking RetrieveTourBookingByConID(string ConBookingID)
        {
            return _context.TourConBooking.Find(ConBookingID);
        }

        public List<TourConBooking> RetrieveAllTour()
        {
            return _context.TourConBooking.ToList();
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
