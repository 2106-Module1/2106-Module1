using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.DataSource
{
    public class RoomGateway : IRoomGateway
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

        public Room FindRoomSummary(int roomNumber, string roomType)
        {
            return appDbContext.RoomsDb().SingleOrDefault(entity => entity.RoomNumberDetail() == roomNumber && entity.RoomTypeDetail() == roomType);
        }

        public IEnumerable<Room> FindAvailability(int floor, string roomType, bool isSmoking, int roomCapacity)
        {
            return appDbContext.RoomsDb().Where(entity => (entity.RoomNumberDetail().ToString()[0].ToString() == floor.ToString()) && entity.RoomTypeDetail() == roomType && entity.SmokingDetail() == isSmoking && entity.CapacityDetail() == roomCapacity && entity.StatusDetail() == "Available");
        }

        public void Insert(Room newRoom)
        {
            if (newRoom != null)
            {
                appDbContext.RoomsDb().Add(newRoom);
                appDbContext.SaveChanges();
            }
        }

        public void Update(Room modifiedRoom)
        {
            if(modifiedRoom != null)
            {
                appDbContext.RoomsDb().Update(modifiedRoom);
                appDbContext.SaveChanges();
            }
        }

        public void Delete(Room room)
        {
            if (room != null)
            {
                appDbContext.RoomsDb().Remove(room);
                appDbContext.SaveChanges();
            }
        }

        public Room FindRoomSummary(int roomId)
        {
            return appDbContext.RoomsDb().SingleOrDefault(entity => entity.RoomIDDetail() == roomId);
        }
    }
}
