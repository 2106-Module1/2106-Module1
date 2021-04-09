using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.Domain
{
    public interface IRoomBuilder
    {
        /// <summary>
        /// Set room capacity to build
        /// </summary>
        /// <param name="roomCapacity"></param>
        void Capacity(int roomCapacity);

        /// <summary>
        /// Set room price to build
        /// </summary>
        /// <param name="roomPrice"></param>
        void Price(int roomPrice);

        /// <summary>
        /// Set room status to build
        /// </summary>
        /// <param name="roomStatus"></param>
        void Status(string roomStatus);

        /// <summary>
        /// Set smoking status of room to build
        /// </summary>
        /// <param name="isSmoking"></param>
        void SmokingPreference(bool isSmoking);

        /// <summary>
        /// Return a Room object after building
        /// </summary>
        /// <returns>Room</returns>
        Room Build();
    }
}
