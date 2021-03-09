using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
{
    interface IPinService
    {
        bool checkPinState();
        void changePinState(bool pinState);
    }
}
