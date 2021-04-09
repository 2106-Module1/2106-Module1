using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;

/*
 * Owner of ReservationService Class: Mod 1 Team 4
 */
namespace HotelManagementSystem.Domain
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

        public Reservation GetLatestReservation()
        {
            return _reservationRepository.GetLatest();
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

        public bool UpdateReservationStatus(int resId, string status)
        {
            // Retrieve Reservation Record and update 
            Reservation resRecord = SearchByReservationId(resId);
            if (resRecord != null && (status == "Fulfilled" || status == "Unfulfilled" || status == "Cancelled"))
            {
                resRecord.UpdateReservation(newStatus: status);

                // update Database
                Update(resRecord);

                return true;
            }
            return false;
        }

        public bool UpdateReservation(int resId, int pax, string roomType, DateTime startDate, DateTime endDate,
            string remarks, DateTime modifiedDate, string promoCode, double price, string status)
        {
            // Retrieve Reservation Record and update 
            Reservation resRecord = SearchByReservationId(resId);

            if (resRecord != null)
            {
                resRecord.UpdateReservation(pax, roomType, startDate, endDate, remarks, modifiedDate, promoCode, price, status);

                // update Database 
                Update(resRecord);

                return true;
            }
            return false;
        }

        public IEnumerable<Reservation> GetTodayReservationByStatus(string status)
        {
            return _reservationRepository.GetByTodayReservations(status);
        }

        public IEnumerable<Reservation> GetReservationStatusByDate(string status, DateTime start, DateTime end)
        {
            return _reservationRepository.GetStatusByDate(status, start, end);
        }

        public bool DeleteReservation(int id)
        {
            Reservation delRes = _reservationRepository.GetById(id);
            if ((string)(delRes.GetReservation())["status"] != "Cancelled" || delRes == null)
            {
                return false;

            }
            _reservationRepository.Delete(delRes);
            return true;
        }

        public bool Update(Reservation reservation)
        {
            _reservationRepository.Update(reservation);
            return true;
        }
    }
}
