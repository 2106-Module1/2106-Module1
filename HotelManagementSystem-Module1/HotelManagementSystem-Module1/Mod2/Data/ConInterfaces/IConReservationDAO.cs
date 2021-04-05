using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IConReservationDAO Interface
    */
    public interface IConReservationDAO
    {
        /*
          * <summary>
          * Update ConBooking status to the database
          * </summary>
          * <returns>
          * bool upon succesfully updating item in the database
          * <returns>
         */
        public Task<bool> UpdateConResStatus();
    }
}
