﻿using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.RetrieveInterfaces;
using System.Collections.Generic;

namespace HotelManagementSystem.Models.RetrieveControls
{
    /*
    * Author: Mod 2 Team 2
    * RetrieveByID Class
    */
    public class RetrieveByID : IRetrieve
    {
        readonly List<ConBooking> _list = new List<ConBooking>();
        public RetrieveByID(List<ConBooking> list)
        {
            _list = list;
        }
        public List<ConBooking> Retrieve(string filter)
        {
            List<ConBooking> output = new List<ConBooking>();
            foreach (ConBooking item in _list)
            {
                if (item.RetrieveConObject().GuestId.ToString().Equals(filter))
                {
                    output.Add(item);
                }
            }
            return output;
        }
    }
}