using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.DataSource
{
    public interface IGuestRepository : IRepository<Guest>
    {
        IEnumerable<Guest> GetByName(string name);
        IEnumerable<Guest> GetByPassportNumber(string passportNumber);

        
    }
}
