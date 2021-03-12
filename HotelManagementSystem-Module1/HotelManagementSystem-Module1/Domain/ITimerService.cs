using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain
{
    interface ITimerService
    {
        bool checkPinState();
        void changePinState(bool pinState);
    }
}
