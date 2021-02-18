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
            //retrieve example : pass in parameters  (floor, twin, smoking, room capacity)
            IEnumerable<Room> retrievedList = roomGateway.FindAvailability(1, "Twin", false, 2);
            roomTable.UpdateRoomList(retrievedList);

            return View("ViewAvailability",roomTable);

        }

        [HttpPost]
        public IActionResult postViewAailability()
        {

            int floor = Convert.ToInt32(Request.Form["txtFloor"].ToString());
            string roomType = Request.Form["selectRoomType"].ToString();
            bool smokingRoom = Convert.ToBoolean(Request.Form["txtSmokingRoom"].ToString());
            int capacity = Convert.ToInt32(Request.Form["txtRoomCap"].ToString());

            //roomTable.ViewAvailability(floor, roomType, smokingRoom, capacity);

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
