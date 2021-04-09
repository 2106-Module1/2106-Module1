using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.RetrieveInterfaces;
using System.Collections.Generic;

namespace HotelManagementSystem.Models.RetrieveControls
{
    /*
    * Author: Mod 2 Team 2
    * RetrieveByStatus Class
    */
    public class RetrieveByStatus : IRetrieve
    {
        readonly List<ConBooking> _list = new List<ConBooking>();
        public RetrieveByStatus(List<ConBooking> list)
        {
            _list = list;
        }
        public List<ConBooking> Retrieve(string filter)
        {
            List<ConBooking> output = new List<ConBooking>();
            foreach (ConBooking item in _list)
            {
                if (item.RetrieveConObject().BookingStatus.ToLower().Equals(filter.ToLower()))
                {
                    output.Add(item);
                }
            }
            return output;
        }
    }
}