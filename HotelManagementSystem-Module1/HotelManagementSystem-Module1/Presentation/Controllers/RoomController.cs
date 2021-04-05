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
            if (TempData["message"] != null)
            {
                ViewBag.message = TempData["message"].ToString();
            }
            IRoom roomTable = roomFacade.RetrieveAllRoom();
            return View("ViewRoomSummary", roomTable);
        }

        [HttpGet]
        [Route("Room/ViewRoomSummary/GetRoomSummary/{roomID:int}")]
        public IActionResult GetRoomSummary(int roomID = 0)
        {
            if (TempData["updateMessage"] != null)
            {
                ViewBag.message = TempData["updateMessage"].ToString();
            }
            IRoom roomTable = roomFacade.FindRoomSummary(roomID);
            return View("GetRoom", roomTable);
        }

        [HttpGet]
        [Route("Room/ViewRoomSummary/GetRoomSummary/UpdateRoom/{roomID:int}")]
        public IActionResult UpdateRoom(int roomID = 0)
        {
            if (TempData["updateMessage"] != null)
            {
                ViewBag.message = TempData["updateMessage"].ToString();
            }
            IRoom roomTable = roomFacade.FindRoomSummary(roomID);
            return View("UpdateRoom", roomTable);
        }

        [HttpPost]
        public IActionResult UpdateDetails()
        {
            int RoomIDDetail = 0;
            int RoomNumberDetail = 0;
            string RoomTypeDetail = "";
            int RoomPriceDetail = 0;
            int RoomCapacityDetail = 0;
            string RoomStatusDetail = "";
            bool RoomSmokingDetail = false;

            RoomIDDetail = Convert.ToInt32(Request.Form["RoomIDDetail"].ToString());
            RoomNumberDetail = Convert.ToInt32(Request.Form["RoomNumberDetail"].ToString());
            RoomTypeDetail = Request.Form["RoomTypeDetail"].ToString();
            RoomPriceDetail = Convert.ToInt32(Request.Form["RoomPriceDetail"].ToString());
            RoomCapacityDetail = Convert.ToInt32(Request.Form["RoomCapacityDetail"].ToString());
            RoomStatusDetail = Request.Form["RoomStatusDetail"].ToString();
            RoomSmokingDetail = Convert.ToBoolean(Request.Form["RoomSmokingDetail"].ToString());

            if(roomFacade.UpdateRoom(RoomIDDetail, RoomTypeDetail, RoomPriceDetail, RoomCapacityDetail, RoomStatusDetail, RoomSmokingDetail))
            {
                TempData["updateMessage"] = "Update Successful";
                return Redirect("ViewRoomSummary/GetRoomSummary/" + RoomIDDetail);

            }
            TempData["updateMessage"] = "Update Unsuccessful";
            return Redirect("ViewRoomSummary/GetRoomSummary/UpdateRoom/" + RoomIDDetail);
        }

        [HttpGet]
        [Route("Room/ViewRoomSummary/GetRoomSummary/DeleteRoom/{roomID:int}")]
        public IActionResult DeleteRoom(int roomID = 0)
        {
            if (roomFacade.DeleteRoom(roomID))
            {
                TempData["message"] = "Deleted Successfully";
            }
            else
            {
                TempData["message"] = "Delete Failed";
            }

            return Redirect("/Room/ViewRoomSummary");
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
