using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Data.ConInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Data.Mod2Repository;

namespace HotelManagementSystem.Data.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * ShuttleBusGateway Class
    */
    public class ShuttleBusGateway : IShuttleBusDAO
    {
        private readonly Mod2Context _context;
        public ShuttleBusGateway(Mod2Context context)
        {
            _context = context;
        }

        public async Task<bool> InsertShuttleBus(ShuttleBus shuttleBus)
        {
            //delayed actual implementation due to this being out of scope for now
            _context.Add(shuttleBus);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveShuttleBusById(string busId)
        {
            //delayed actual implementation due to this being out of scope for now
            return true;
        }

        public int RetrieveTotalFleetCapacity()
        {
            return _context.ShuttleBus.AsEnumerable().Sum(x => x.RetrieveShuttleBusCapacity());   //not tested to work
        }

        public List<ShuttleBus> RetrieveAllShuttleBuses()
        {
            return _context.ShuttleBus.ToList();
        }

        public ShuttleBus RetrieveShuttleBusById(string busId)
        {
            return _context.ShuttleBus.AsEnumerable().Where(x => x.RetrieveId() == busId).FirstOrDefault(); //'should' return only one item. since id should be unique.
        }

        public string ReturnIndexOfLastItem()
        {
            ShuttleBus finalObject = _context.ShuttleBus.AsEnumerable().OrderByDescending(x => x.RetrieveId()).FirstOrDefault();

            return finalObject.RetrieveId();

        }

    }
}
