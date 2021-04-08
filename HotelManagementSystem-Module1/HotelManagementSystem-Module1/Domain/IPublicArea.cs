using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 3 Team 5
 */
namespace HotelManagementSystem.Domain
{
    public interface IPublicArea
    {
        List<PublicAreaDTO> getAllFacilityResults();
    }
}
