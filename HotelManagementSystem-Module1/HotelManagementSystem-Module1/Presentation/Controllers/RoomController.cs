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
        private readonly IRoomFacade roomFacade;
        public RoomController(IRoomFacade inRoomFacade)
        {
            roomFacade = inRoomFacade;
        }

        [HttpGet]
        public IActionResult ViewAvailability()
        {
            IRoom roomTable = roomFacade.RetrieveAvailableRoom();

            return View("ViewAvailability",roomTable);

        }

        [HttpPost]
        public IActionResult postViewAvailability()
        {

            int floor = 0;
            int capacity = 0;
            string roomType = "";
            bool smokingRoom = false;

            floor = Convert.ToInt32(Request.Form["txtFloor"].ToString());
            roomType  = Request.Form["selectRoomType"].ToString();
            smokingRoom = Convert.ToBoolean(Request.Form["txtSmokingRoom"].ToString());
            capacity = Convert.ToInt32(Request.Form["txtRoomCap"].ToString());


            IRoom roomTable = roomFacade.RetrieveAvailableRoom(floor, roomType, smokingRoom, capacity);

            return View("ViewAvailability", roomTable);


        }

        public IActionResult ViewRoomSummary()
        {
            IRoom roomTable = roomFacade.RetrieveAllRoom();
            return View("ViewRoomSummary", roomTable);
        }

        [HttpGet]
        [Route("Room/ViewRoomSummary/GetRoomSummary/{roomID:int}")]
        public IActionResult GetRoomSummary(int roomID = 0)
        {
            IRoom roomTable = roomFacade.FindRoomSummary(roomID);
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
