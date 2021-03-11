using HotelManagementSystem.Models;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain;
using System.Text.RegularExpressions;

namespace HotelManagementSystem.Presentation.Controllers
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

            int floor = 0;
            int capacity = 0;
            string roomType = "";
            bool smokingRoom = false;

            floor = Convert.ToInt32(Request.Form["txtFloor"].ToString());
            roomType  = Request.Form["selectRoomType"].ToString();
            smokingRoom = Convert.ToBoolean(Request.Form["txtSmokingRoom"].ToString());
            capacity = Convert.ToInt32(Request.Form["txtRoomCap"].ToString());

         
            IEnumerable<Room> retrievedList = roomGateway.FindAvailability(floor, roomType, smokingRoom, capacity);
            roomTable.UpdateRoomList(retrievedList);


            return View("ViewAvailability", roomTable);


        }

        public IActionResult ViewRoomSummary()
        {
            IEnumerable<Room> retrievedList = roomGateway.GetAllRooms();
            roomTable.UpdateRoomList(retrievedList);
            return View("ViewRoomSummary", roomTable);
        }

        [HttpGet]
        [Route("Room/ViewRoomSummary/GetRoom/{roomID:int}")]
        public IActionResult GetRoom(int roomID = 0)
        {
            Room retrievedRoom = roomGateway.FindRoomSummary(roomID);
            IEnumerable<Room> roomList = new Room[]{ retrievedRoom };
            roomTable.UpdateRoomList(roomList);
            return View("GetRoom", roomTable);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool isDigit(int digit)
        {   
            Regex rx = new Regex(@"^[0-9]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(digit.ToString());
        }
        
    }
}
