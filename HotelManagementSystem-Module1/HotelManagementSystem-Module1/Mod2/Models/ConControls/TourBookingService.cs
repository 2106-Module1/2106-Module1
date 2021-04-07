using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Data.ConInterfaces;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using HotelManagementSystem.Models.RetrieveInterfaces;
using HotelManagementSystem.Models.RetrieveControls;
using HotelManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * TourBookingService Class
    */
    public class TourBookingService : ITourServices
    {
        private readonly static DAOFactory EntityFrameworkDAOFactory = DAOFactory.GetDAOFactory(DAOFactory.ENTITY);
        private readonly ITourReservationDAO _TourBookingDAO = (TourReservationGateway)EntityFrameworkDAOFactory.GetTourReservationDAO();

        public TourBookingService(ITourReservationDAO TourBookingDAO)
        {
            _TourBookingDAO = TourBookingDAO;
        }

        public string GenerateID(int guesitID)
        {
            string GenID = long.Parse(DateTime.Now.ToString("yyyyMMddHHmm")).ToString();

            // Concierge DateTimeString GuestID
            return "C" + GenID + guesitID.ToString();
        }

        public async Task<bool> AddTourBooking(TourConBooking tourConBooking)
        {
            if(await _TourBookingDAO.InsertTourConRes(tourConBooking))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ConBooking> RetrieveTour(string field, string filter)
        {
            // Converting the TaxiConBooking to ConBooking
            List<ConBooking> list = new List<ConBooking>();
            foreach (ConBooking item in _TourBookingDAO.RetrieveAllTour())
            {
                list.Add(item);
            }
            // Using the Strategy pattern to choose the right strategy 
            // to retrieve and filter the results
            IRetrieve retrieve = new RetrieveAll(list);
            if (filter.Equals(""))
            {
                return retrieve.Retrieve(filter);
            }
            else if (field.Equals("name"))
            {
                retrieve = new RetrieveByName(list);
                return retrieve.Retrieve(filter);
            }
            else if (field.Equals("date"))
            {
                retrieve = new RetrieveByDate(list);
                return retrieve.Retrieve(filter);
            }
            else if (field.Equals("id"))
            {
                retrieve = new RetrieveByID(list);
                return retrieve.Retrieve(filter);
            }
            else if (field.Equals("status"))
            {
                retrieve = new RetrieveByStatus(list);
                return retrieve.Retrieve(filter);
            }
            else
            {
                return retrieve.Retrieve(filter);
            }
        }

        public TourConBooking RetrieveTourBookingByConID(string ConBookingID)
        {
            return _TourBookingDAO.RetrieveTourBookingByConID(ConBookingID);
        }

        public async Task<bool> UpdateBookingStatusCancelled(string conBookingID, string pin)
        {
            //Mod 1 Auth Func Not Implemented due to complexity of Dependecy Injection ( Demo to mock feature )
            if (!pin.Equals("") /* && auth.AuthenticatePin("ValidatePin", pin)*/)
            {
                _TourBookingDAO.RetrieveTourBookingByConID(conBookingID).SetBookingStatus("CANCELLED");
                if(await _TourBookingDAO.UpdateConResStatus())
                {
                    return true;
                }
                // gateway error
                else
                {
                    return false;
                }
            }
            // invalid pin
            else
            {
                return false;
            }
        }

    }
}
