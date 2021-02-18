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
        private readonly IRoomGateway roomGateway;
        public RoomController(IRoom inRoomTable, IRoomGateway inRoomGateway)
        {
            roomTable = inRoomTable;
            roomGateway = inRoomGateway;
        }

        [HttpGet]
        public IActionResult ViewAvailability()
        {

            //Dictionary<string, object> viewTemp = new Dictionary<string, object>();


            //viewTemp.Add("Floor", default(int));
            //viewTemp.Add("Room Type", default(string));
            //viewTemp.Add("Room Capacity", default(int));
            //viewTemp.Add("Smoking Room", default(string));

            //ViewBag.viewTemp = viewTemp;
            //ViewBag.lst = roomTable.ViewAvailability(1, "Twin", false, 1);
            IEnumerable<Room> retrievedList = roomGateway.FindAvailability(1, "Twin", false, 1);
            roomTable.UpdateRoomList(retrievedList);

            return View("ViewAvailability",roomTable);

        }

        [HttpPost]
        public IActionResult ViewAvailability(Dictionary<string, object> newView)
        {
            //Recreate the fields again after post
            Dictionary<string, object> viewTemp = new Dictionary<string, object>();
            viewTemp.Add("Floor", default(int));
            viewTemp.Add("Room Type", default(string));
            viewTemp.Add("Room Capacity", default(int));
            viewTemp.Add("Smoking Room", default(string));
            ViewBag.viewTemp = viewTemp;

            int floor = Convert.ToInt32(Request.Form["Floor"].ToString());
            string roomType = Request.Form["Room Type"].ToString();
            bool smokingRoom = Convert.ToBoolean(Request.Form["Smoking Room"].ToString());
            int capacity = Convert.ToInt32(Request.Form["Room Capacity"].ToString());

            ViewBag.lst = roomTable.ViewAvailability(floor, roomType, smokingRoom, capacity);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
