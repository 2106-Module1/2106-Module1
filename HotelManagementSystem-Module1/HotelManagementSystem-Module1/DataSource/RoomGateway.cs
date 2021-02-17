using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public class RoomGateway
    {
        private readonly IAppDbContext appDbContext;

        public RoomGateway(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return appDbContext.RoomsDb().AsEnumerable();
        }
    }
}
