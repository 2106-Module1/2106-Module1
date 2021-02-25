using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
{
    public interface IPublicArea
    {
        List<PublicAreaDTO> getAllFacilityResults();
    }
}
