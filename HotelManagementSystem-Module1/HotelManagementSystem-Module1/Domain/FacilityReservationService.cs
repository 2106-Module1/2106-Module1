using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem_Module1.Domain
{
    public class FacilityReservationService : IFacilityReservationService
    {
        private readonly IFacilityReservationRepository _facilityReservationRepository;

        public FacilityReservationService(IFacilityReservationRepository facilityReservationRepository)
        {
            _facilityReservationRepository = facilityReservationRepository;
        }

        public bool CheckValidReservation(FacilityReservation facilityReservation)
        {
            //TODO : check database for clashing reservation timings
            if (_facilityReservationRepository.GetById(facilityReservation.FacilityIdDetails()) == null)
                return false;

            IEnumerable<FacilityReservation> facilityReservations = RetrieveReservations();
            foreach (FacilityReservation facilityReservationInDB in facilityReservations) {

                // Less than zero t1 is earlier than t2.
                // Zero t1 is the same as t2.
                // Greater than zero t1 is later than t2.
                if ((DateTime.Compare(facilityReservation.StartTimeDetails(), facilityReservationInDB.EndTimeDetails()) <= 0 ) && (DateTime.Compare(facilityReservation.EndTimeDetails(), facilityReservationInDB.StartTimeDetails()) >= 0))  {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteReservation(FacilityReservation facilityReservation)
        {
            if (_facilityReservationRepository.GetById(facilityReservation.FacilityIdDetails()) == null)
                return false;
            _facilityReservationRepository.Delete(facilityReservation);
            return true;
        }

        public bool DeleteReservation(int reservationId)
        {
            FacilityReservation reservation = _facilityReservationRepository.GetById(reservationId);
            if (reservation == null)
                return false;
            _facilityReservationRepository.Delete(reservation);
            return true;
        }

        public bool MakeReservation(FacilityReservation facilityReservation)
        {
            _facilityReservationRepository.Insert(facilityReservation);
            return true;
        }

        public FacilityReservation RetrieveByReservationId(int reservationId)
        {
            return _facilityReservationRepository.GetById(reservationId);
        }

        public IEnumerable<FacilityReservation> RetrieveByReserveeId(int reserveeId)
        {
            return _facilityReservationRepository.GetByReserveeId(reserveeId);
        }

        public IEnumerable<FacilityReservation> RetrieveReservations()
        {
            return _facilityReservationRepository.GetAll();
        }

        public bool UpdateReservation(FacilityReservation facilityReservation)
        {
            if (_facilityReservationRepository.GetById(facilityReservation.FacilityIdDetails()) == null)
                return false;
            _facilityReservationRepository.Update(facilityReservation);
            return true;
        }
    }
}
