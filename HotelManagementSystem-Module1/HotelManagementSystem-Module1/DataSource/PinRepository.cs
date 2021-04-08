using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.DataSource
{
    public class PinRepository : IPinRepository
    {
        private readonly IAppDbContext appDbContext;

        public PinRepository(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }
        /// <summary>
        /// Call the unit of work to save changes
        /// </summary>
        /// <param name="modifiedPin">Pin object with modified pin number</param>
        public void UpdatePin(Pin modifiedPin)
        {
            if (modifiedPin != null)
            { 
                appDbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Retrieve Pin from database
        /// </summary>
        /// <returns>Pin</returns>
        public Pin GetPin()
        {
            return appDbContext.PinDB().AsEnumerable().SingleOrDefault();
        }
        /// <summary>
        /// Check and retrieve pin that has the exact same pin number in database
        /// </summary>
        /// <param name="pinNumber">pin number to match against record in database</param>
        /// <returns>Pin or null</returns>
        public Pin ValidatePin(string pinNumber)
        {
            return appDbContext.PinDB().AsEnumerable().SingleOrDefault(entity => entity.PinNumberDetails() == pinNumber);
        }
    }
}
