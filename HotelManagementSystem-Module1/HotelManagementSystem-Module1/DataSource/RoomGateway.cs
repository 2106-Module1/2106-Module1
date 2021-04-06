using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManagementSystem.Domain.Models;

namespace HotelManagementSystem.DataSource
{
    public class RoomGateway : IRoomGateway
    {
        private readonly IAppDbContext appDbContext;

        public RoomGateway(IAppDbContext appContext)
        {
            appDbContext = appContext;
        }
        /// <summary>
        /// Return all room in database
        /// </summary>
        /// <returns>IEnumerable Collection of Room entities</returns>
        public IEnumerable<Room> GetAllRooms()
        {
            return appDbContext.RoomsDb().AsEnumerable();
        }
        /// <summary>
        /// Find specific room based on the room number and type of room
        /// </summary>
        /// <param name="roomNumber"></param>
        /// <param name="roomType"></param>
        /// <returns>Room entity</returns>
        public Room FindRoomSummary(int roomNumber, string roomType)
        {
            return appDbContext.RoomsDb().AsEnumerable().SingleOrDefault(entity => entity.RoomNumberDetail() == roomNumber && entity.RoomTypeDetail() == roomType);
        }
        /// <summary>
        /// Return all room that are available based on param
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="roomType"></param>
        /// <param name="isSmoking"></param>
        /// <param name="roomCapacity"></param>
        /// <returns>IEnumerable Collection of Room entities</returns>
        public IEnumerable<Room> FindAvailability(int floor, string roomType, bool isSmoking, int roomCapacity)
        {
            return appDbContext.RoomsDb().AsEnumerable().Where(entity => (entity.RoomNumberDetail().ToString()[0].ToString() == floor.ToString()) && entity.RoomTypeDetail() == roomType && entity.SmokingDetail() == isSmoking && entity.CapacityDetail() == roomCapacity && entity.StatusDetail() == "Available");
        }
        /// <summary>
        /// Return all room in database that are available
        /// </summary>
        /// <returns>IEnumerable Collection of Room entities</returns>
        public IEnumerable<Room> FindAvailability()
        {
            return appDbContext.RoomsDb().AsEnumerable().Where(entity => (entity.StatusDetail() == "Available"));
        }
        /// <summary>
        /// Create a new room record in database
        /// </summary>
        /// <param name="newRoom"></param>
        public void Insert(Room newRoom)
        {
            if (newRoom != null)
            {
                appDbContext.RoomsDb().Add(newRoom);
                appDbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Call the unit of work to save changes
        /// </summary>
        public void Update()
        {
             appDbContext.SaveChanges();
        }
        /// <summary>
        /// Remove room from database
        /// </summary>
        /// <param name="room">room to be deleted</param>
        public void Delete(Room room)
        {
            if (room != null)
            {
                appDbContext.RoomsDb().Remove(room);
                appDbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Find specific room based on id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns>Room entity</returns>
        public Room FindRoomSummary(int roomId)
        {
            return appDbContext.RoomsDb().AsEnumerable().Where(entity => entity.RoomIDDetail() == roomId).SingleOrDefault();
        }
    }
}
