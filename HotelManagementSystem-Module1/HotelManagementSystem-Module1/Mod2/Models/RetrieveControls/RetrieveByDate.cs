using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.RetrieveInterfaces;
using System.Collections.Generic;
using System;

namespace HotelManagementSystem.Models.RetrieveControls
{
    /*
    * Author: Mod 2 Team 2
    * RetrieveByDate Class
    */
    public class RetrieveByDate : IRetrieve
    {
        readonly List<ConBooking> _list = new List<ConBooking>();
        public RetrieveByDate(List<ConBooking> list)
        {
            _list = list;
        }
        public List<ConBooking> Retrieve(string filter)
        {
            List<ConBooking> output = new List<ConBooking>();
            foreach (ConBooking item in _list)
            {
                string[] s = item.RetrieveConObject().ActivityDateTime.ToString().Split(" ");
                if (formatDate(s[0], 2).Contains(formatDate(filter, 1)))
                {
                    output.Add(item);
                }
            }
            return output;
        }
        private string formatDate (string oldDate, int option)
        {
            if (option == 1)
            {
                string[] parts = oldDate.Split("-");
                return parts[2] + "/" + parts[1] + "/" + parts[0];
            }
            else
            {
                string[] parts = oldDate.Split("/");
                parts[0] = Convert.ToInt16(parts[0]) < 10 ? "0" + parts[0] : parts[0];
                parts[1] = Convert.ToInt16(parts[1]) < 10 ? "0" + parts[1] : parts[1];
                return parts[0] + "/" + parts[1] + "/" + parts[2];
            }
        }
    }
}