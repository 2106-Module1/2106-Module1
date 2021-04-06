using HotelManagementSystem.Data;
using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Data.ConInterfaces;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * ShuttleBusService Class
    */
    public class ShuttleBusService : IShuttleBusServices
    {
        private readonly static DAOFactory EntityFrameworkDAOFactory = DAOFactory.GetDAOFactory(DAOFactory.ENTITY);
        private readonly IShuttleBusDAO _shuttleBusDAO = (ShuttleBusGateway)EntityFrameworkDAOFactory.GetShuttleBusDAO();

        public ShuttleBusService(IShuttleBusDAO shuttleBusDAO)
        {
            _shuttleBusDAO = shuttleBusDAO;
        }

        /*
         * <summary>
         * Generate a unique ID for ShuttleBus object
         * </summary>
         * <returns>
         * String combining the ID number following the last index in the ShuttleBus database
         * THIS ASSUMES THAT THE ID IS PURELY NUMERICAL
         * WE SHOULD PROBABLY SETTLE ON AN ID FORMAT OR MAKE EXCEPTIONS FOR THAT
         * but still low priority. this is only useful if we add/delete buses.
         * <returns>
        */
        /*public string GenerateID()
        {
            string GenID = "";
            int index = 0;
            index = Int32.Parse(_shuttleBusDAO.ReturnIndexOfLastItem()) + 1;

            return index.ToString();
        }*/

        /// <summary>
        /// Request to adds a new Shuttle Bus into the database
        /// </summary>
        /// <returns>
        /// returns true if adding is successful. returns false if not.
        /// </returns>
        public async Task<bool> AddShuttleBus(ShuttleBus shuttleBus)
        {
            //not scoped for implementation
            return true;
        }

        /// <summary>
        /// Request to remove a new Shuttle Bus into the database
        /// </summary>
        /// <returns>
        /// returns true if removal  is successful. returns false if not.
        /// </returns>
        public async Task<bool> RemoveShuttleBus(ShuttleBus shuttleBus)
        {
            //not scoped for implementation
            return true;
        }

    }
}
