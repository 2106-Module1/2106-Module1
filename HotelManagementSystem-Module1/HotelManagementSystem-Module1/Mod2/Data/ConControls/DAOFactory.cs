using HotelManagementSystem.Data.ConInterfaces;

namespace HotelManagementSystem.Data.ConControls
{
    /*
    * Author: Mod 2 Team 2
    * DAOFactory Abstract Class
    */
    public abstract class DAOFactory
    {
        public abstract IRestReservationDAO GetRestReservationDAO();
        public abstract ITourReservationDAO GetTourReservationDAO();
        public abstract ITaxiReservationDAO GetTaxiReservationDAO();
        public abstract IShuttleScheduleDAO GetShuttleScheduleDAO();
        public abstract IShuttleBusDAO GetShuttleBusDAO();
        public abstract IShuttlePassengerDAO GetShuttlePassengerDAO();


        // List of DAO types supported by the factory
        public static int ENTITY = 1;

        // NOT IMPLEMENTED FOR EXAMPLE
        public static int ORACLE = 2;
        public static int SYBASE = 3;
        public static DAOFactory GetDAOFactory(int whichFactory)
        {
            return whichFactory switch
            {
                1 => new EntityFrameworkDAOFactory(),
/*                2 => new OracleDAOFactory(),
 *                3 => new SYSBASEDAOFactory(),
*/                _ => null,
            };
        }
    }
}
