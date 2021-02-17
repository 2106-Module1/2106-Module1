using HotelManagementSystem_Module1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
{
    public interface IReservationService
    {
        IEnumerable<Reservation> GetAllReservations();

        IEnumerable<Reservation> SearchByReservationId(int id);

        IEnumerable<Reservation> SearchByGuestId(int id);

        bool CreateReservation(Reservation reservation);

        bool DeleteReservation(int id);

        public bool UpdateReservation(Reservation reservation);
    }
}
