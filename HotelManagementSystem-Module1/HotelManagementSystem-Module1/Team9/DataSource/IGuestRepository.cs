using HotelManagementSystem_Module1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Team9.DataSource
{
    public interface IGuestRepository<T> : IRepository<T> where T : class
    {
        IEnumerable<T> GetByName(string name);
        IEnumerable<T> GetByPassportNumber(string passportNumber);
    }
}
