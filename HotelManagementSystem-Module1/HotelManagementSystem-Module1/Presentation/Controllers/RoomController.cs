using HotelManagementSystem_Module1.Models;
using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.DataSource;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain;

namespace HotelManagementSystem_Module1.Presentation.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoom roomTable;
        public RoomController(IRoom inRoomTable)
        {
            roomTable = inRoomTable;
        }
        public IActionResult ViewAvailability()
        {
            
            Room room = new Room(1, 101, "single",  25, 1, "a", false);
            Room room1 = new Room(1, 101, "twin", 25, 1, "a", false);
            Room room2 = new Room(1, 101, "single", 25, 1, "b", false);
            List<Room> lst = new List<Room>();
            if (roomTable.CreateRoom(101,"Twin", 25, 1, "Available", false))
            {
                
            }
            //roomTable.CreateRoom(room);
            //lst.add(room);
            //lst.add(room1);
            //lst.add(room2);
            ViewBag.lst = lst;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
