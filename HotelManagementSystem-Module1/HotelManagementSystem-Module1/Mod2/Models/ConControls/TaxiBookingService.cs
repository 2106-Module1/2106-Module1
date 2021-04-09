using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Models.ConEntities;
using HotelManagementSystem.Models.ConInterfaces;
using HotelManagementSystem.Models.RetrieveInterfaces;
using HotelManagementSystem.Models.RetrieveControls;
using HotelManagementSystem.Domain;
using System;
using System.Collections.Generic;
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

        public string GenerateID(int guesitID)
        {
            string GenID = long.Parse(DateTime.Now.ToString("yyyyMMddHHmm")).ToString();

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

        public async Task<bool> UpdateBookingStatusCancelled(string conBookingID, string pin)
        {
            //Mod 1 Auth Func Not Implemented due to complexity of Dependecy Injection ( Demo to mock feature )
            if (!pin.Equals("") /* && auth.AuthenticatePin("ValidatePin", pin)*/ )
            {
                _TaxiBookingDAO.RetrieveTaxiBookingByConID(conBookingID).SetBookingStatus("CANCELLED");
                if (await _TaxiBookingDAO.UpdateConResStatus())
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
