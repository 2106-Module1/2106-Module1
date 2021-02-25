﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.DataSource;
using HotelManagementSystem_Module1.Domain.Models;

/*
 * Owner of Control Class: Mod 1 Team 4
 */
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

        public IEnumerable<Reservation> GetReservationByStatus(string status)
        {
            return _reservationRepository.GetByStatus(status);
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