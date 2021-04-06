using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 3 Team 5
 * Hardcoded dataset
 */
namespace HotelManagementSystem.Domain
{
    public class PublicArea : IPublicArea
    {
        public List<PublicAreaDTO> getAllFacilityResults()
        {
            List<PublicAreaDTO> tempList = new List<PublicAreaDTO>
            {
                new PublicAreaDTO(1, "Swimming Pool", "", 1, true, 10),
                new PublicAreaDTO(2, "Spa", "", 1, true, 10),
                new PublicAreaDTO(3, "Ballroom", "", 1, true, 50),
                new PublicAreaDTO(4, "Gym", "", 1, true, 10),
                new PublicAreaDTO(34, "Dine - In Restaurant", "", 1, true, 30)
            };
            return tempList;
        }
    }
}
