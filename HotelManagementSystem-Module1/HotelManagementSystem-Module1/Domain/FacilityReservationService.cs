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

        public void CheckValidReservation(FacilityReservation facilityReservation)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(FacilityReservation facilityReservation)
        {
            _facilityReservationRepository.Delete(facilityReservation);
        }

        public void DeleteReservation(int reservationId)
        {
            _facilityReservationRepository.Delete(_facilityReservationRepository.GetById(reservationId));
        }

        public void MakeReservation(FacilityReservation facilityReservation)
        {
            _facilityReservationRepository.Insert(facilityReservation);
        }

        public IEnumerable<FacilityReservation> RetrieveReservations()
        {
            return _facilityReservationRepository.GetAll();
        }

        public void UpdateReservation(FacilityReservation facilityReservation)
        {
            _facilityReservationRepository.Update(facilityReservation);
        }
    }
}
