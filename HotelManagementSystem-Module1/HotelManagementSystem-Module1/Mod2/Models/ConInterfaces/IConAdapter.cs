using HotelManagementSystem.Models.ConEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConInterfaces
{
    /*
    * Author: Mod 2 Team 2
    * IConAdapter Interface
    */
    public interface IConAdapter
    {
        /*
         * <summary>
         * Takes in the inputs from the presentation layer as a dictionary 
         * and create the ConBooking Object
         * </summary>
         * <returns>
         * ConBooking
         * <returns>
        */
        public ConBooking GetConBooking();
    }
}
