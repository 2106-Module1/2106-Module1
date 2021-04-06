using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using HotelManagementSystem.Models.RetrieveInterfaces;
using HotelManagementSystem.Models.RetrieveControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Data.ConInterfaces;

namespace HotelManagementSystem.Models.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * TaxiBookingService Class
    */
    public class TaxiBookingService : ITaxiServices
    {
        private readonly static DAOFactory EntityFrameworkDAOFactory = DAOFactory.GetDAOFactory(DAOFactory.ENTITY);
        private readonly ITaxiReservationDAO _TaxiBookingDAO = (TaxiReservationGateway)EntityFrameworkDAOFactory.GetTaxiReservationDAO();

        public TaxiBookingService(ITaxiReservationDAO TaxiBookingDAO)
        {
            _TaxiBookingDAO = TaxiBookingDAO;
        }

        public string GenerateID(DateTime datetime, int guesitID)
        {
            string GenID = long.Parse(datetime.ToString("yyyyMMddHHmm")).ToString();

            // Concierge DateTimeString GuestID
            return "C" + GenID + guesitID.ToString();
        }

        public async Task<bool> AddTaxiBooking(TaxiConBooking taxiConBooking)
        {
            if(await _TaxiBookingDAO.InsertTaxiConRes(taxiConBooking))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ConBooking> RetrieveTaxi(string field, string filter)
        {
            // Converting the TaxiConBooking to ConBooking
            List<ConBooking> list = new List<ConBooking>();
            foreach (ConBooking item in _TaxiBookingDAO.RetrieveAllTaxi())
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


        public TaxiConBooking RetrieveTaxiBookingByConID(string ConBookingID)
        {
            return _TaxiBookingDAO.RetrieveTaxiBookingByConID(ConBookingID);
        }

        public async Task<bool> UpdateBookingStatusCancelled(string conBookingID)
        {
            _TaxiBookingDAO.RetrieveTaxiBookingByConID(conBookingID).SetBookingStatus("CANCELLED");
            if(await _TaxiBookingDAO.UpdateConResStatus())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
