using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult ViewAvailability()
        {
            IRoom roomTable = new RoomTable();
            Room room = new Room();
            
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
