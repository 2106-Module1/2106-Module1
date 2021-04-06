using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Owner : Mod 1 Team 9
 */
namespace HotelManagementSystem.Domain
{
    public class FacilityReservationService : IFacilityReservationService
    {
        private readonly IFacilityReservationRepository _facilityReservationRepository;
        private readonly IPublicArea _publicArea;

        public FacilityReservationService(IFacilityReservationRepository facilityReservationRepository, IPublicArea publicArea)
        {
            _facilityReservationRepository = facilityReservationRepository;
            _publicArea = publicArea;
        }

        public bool CheckValidReservation(FacilityReservation facilityReservation)
        {
            int currentPax = 0;
            int maxPax = 0;

            // get max pax of facility
            List<PublicAreaDTO> fullfacilityList = _publicArea.getAllFacilityResults();
            foreach (PublicAreaDTO fac in fullfacilityList)
            {
                if (fac.public_area_id == facilityReservation.FacilityIdDetails())
                {
                    maxPax = fac.max_pax;
                }
            }

            // get current number of pax of facility in timeslot
            IEnumerable<FacilityReservation> facilityReservations = RetrieveReservations();
            foreach (FacilityReservation facilityReservationInDB in facilityReservations) {
                if (facilityReservationInDB.FacilityIdDetails() == facilityReservation.FacilityIdDetails()) {

                    // check if same timeslot
                    if ((DateTime.Compare(facilityReservation.StartTimeDetails(), facilityReservationInDB.EndTimeDetails()) <= 0) && (DateTime.Compare(facilityReservation.EndTimeDetails(), facilityReservationInDB.StartTimeDetails()) >= 0))
                    {
                        currentPax += facilityReservationInDB.NumberOfPax();
                    }
                }
            }
            // sum up total pax if including new reservation
            int totalPax = currentPax + facilityReservation.NumberOfPax();

            // check if exceed max pax
            if (totalPax > maxPax) {
                return false;
            }
            else{
                return true;
            }
        }

        public bool DeleteReservation(FacilityReservation facilityReservation)
        {
            if (_facilityReservationRepository.GetById(facilityReservation.ReservationIdDetails()) == null)
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
            if (CheckValidReservation(facilityReservation)) {
                _facilityReservationRepository.Insert(facilityReservation);  
            }
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
            if (_facilityReservationRepository.GetById(facilityReservation.ReservationIdDetails()) == null)
                return false;
            _facilityReservationRepository.Update(facilityReservation);
            return true;
        }
    }
}
