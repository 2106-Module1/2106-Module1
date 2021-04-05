using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IShuttleAdapter Interface
    */
    public interface IShuttleAdapter
    {
        /*
         * <summary>
         * Takes in the inputs from the presentation layer as a dictionary 
         * and create the ShuttleBooking
         * </summary>
         * <returns>
         * ConBooking
         * <returns>
        */
        public ShuttleSchedule GetShuttleSchedule();
    }
}
