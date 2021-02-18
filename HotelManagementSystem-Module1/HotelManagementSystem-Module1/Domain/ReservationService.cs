using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.DataSource;
using HotelManagementSystem_Module1.Domain.Models;

namespace HotelManagementSystem_Module1.Domain
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _reservationRepository.GetAll();
        }

        public Reservation SearchByReservationId(int id)
        {
            return _reservationRepository.GetById(id);
        }

        public IEnumerable<Reservation> SearchByGuestId(int id)
        {
            return _reservationRepository.GetByGuestId(id);
        }

        public bool CreateReservation(Reservation reservation)
        {
            _reservationRepository.Insert(reservation);
            return true;
        }

        public bool DeleteReservation(int id)
        {
            Reservation DelRes = _reservationRepository.GetById(id);
            if ((String) (DelRes.GetReservation())["status"] == "Cancelled")
            {
                _reservationRepository.Delete(DelRes);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateReservation(Reservation reservation)
        {
            _reservationRepository.Update(reservation);
            return true;
        }
    }
}
