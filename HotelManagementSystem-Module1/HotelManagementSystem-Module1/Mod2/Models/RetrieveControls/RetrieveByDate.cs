using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.RetrieveInterfaces;
using System.Collections.Generic;

namespace HotelManagementSystem.Models.RetrieveControls
{
    /*
    * Author: Mod 2 Team 2
    * RetrieveByDate Class
    */
    public class RetrieveByDate : IRetrieve
    {
        List<ConBooking> _list = new List<ConBooking>();
        public RetrieveByDate(List<ConBooking> list)
        {
            _list = list;
        }
        public List<ConBooking> Retrieve(string filter)
        {
            string empty = "";
            if (empty.Equals(filter))
            {
                return _list;
            }
            else
            {
                List<ConBooking> output = new List<ConBooking>();
                foreach (ConBooking item in _list)
                {
                    ConBooking.ConBookingReadOnly temp = item.RetrieveConObject();
                    if (temp.ActivityDateTime.ToString().Equals(filter))
                    {
                        output.Add(item);
                    }
                }
                return output;
            }
        }
    }
}