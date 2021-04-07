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
        List<ConBooking> _list = new List<ConBooking>();
        public RetrieveByStatus(List<ConBooking> list)
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
                    if (temp.BookingStatus.ToString().Equals(filter))
                    {
                        output.Add(item);
                    }
                }
                return output;
            }
        }
    }
}