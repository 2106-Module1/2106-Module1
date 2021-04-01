using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.RetrieveInterfaces;
using System.Collections.Generic;

namespace HotelManagementSystem.Models.RetrieveControls
{
    /*
    * Author: Mod 2 Team 2
    * RetrieveAll Class
    */
    public class RetrieveAll : IRetrieve
    {
        List<ConBooking> _list = new List<ConBooking>();
        public RetrieveAll(List<ConBooking> list)
        {
            _list = list;
        }
        public List<ConBooking> Retrieve(string filter)
        {
            return _list;
        }
    }
}
