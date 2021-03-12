using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.DataSource
{
    public interface IPinRepository
    {
        void UpdatePin(Pin modifiedpin);
        Pin ValidatePin(string pin);
    }
}
