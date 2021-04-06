using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Data.ConInterfaces;
using HotelManagementSystem.Data.Mod2Repository;

namespace HotelManagementSystem.Data.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * EntityFrameworkDAOFactory Class
    */
    public class EntityFrameworkDAOFactory : DAOFactory
    {
        private readonly Mod2Context _context;

        public override IShuttleScheduleDAO GetShuttleScheduleDAO()
        {
            return new ShuttleScheduleGateway(_context);
        }

        public override IShuttleBusDAO GetShuttleBusDAO()
        {
            return new ShuttleBusGateway(_context);
        }

        public override IShuttlePassengerDAO GetShuttlePassengerDAO()
        {
            return new ShuttlePassengerGateway(_context);
        }

        public override IRestReservationDAO GetRestReservationDAO()
        {
            return new RestReservationGateway(_context);
        }

        public override ITaxiReservationDAO GetTaxiReservationDAO()
        {
            return new TaxiReservationGateway(_context);
        }

        public override ITourReservationDAO GetTourReservationDAO()
        {
            return new TourReservationGateway(_context);
        }
    }
}
