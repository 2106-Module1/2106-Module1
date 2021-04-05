using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.Domain
{
    public interface IRoomBuilder
    {
        void Capacity(int roomCapacity);
        void Price(int roomPrice);
        void Status(string roomStatus);
        void SmokingPreference(bool isSmoking);
        Room Build();
    }
}
