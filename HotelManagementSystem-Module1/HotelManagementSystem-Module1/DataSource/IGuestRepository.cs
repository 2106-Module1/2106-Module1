using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.DataSource
{
    public interface IGuestRepository : IRepository<Guest>
    {
        IEnumerable<Guest> GetByName(string name);
        IEnumerable<Guest> GetByPassportNumber(string passportNumber);
    }
}
