using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// This class is provided by Module 3
/// </summary>
namespace HotelManagementSystem.Domain.Models
{
    public class PublicAreaDTO
    {
        public int public_area_id;
        public string public_area_name;
        public string public_area_status;
        public int public_area_floor;
        public bool is_facility;
        public int max_pax;

        public PublicAreaDTO(int public_area_id, string public_area_name, string public_area_status, int public_area_floor, bool is_facility, int max_pax)
        {
            this.public_area_id = public_area_id;
            this.public_area_name = public_area_name;
            this.public_area_status = public_area_status;
            this.public_area_floor = public_area_floor;
            this.is_facility = is_facility;
            this.max_pax = max_pax;
        }
    }
}
