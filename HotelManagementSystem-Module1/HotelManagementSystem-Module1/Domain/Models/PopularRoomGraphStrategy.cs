using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Models
{
    public class PopularRoomGraphStrategy : IAnalyticsStrategy
    {
        

        int[] IAnalyticsStrategy.GenerateChartValues(IEnumerable<Reservation> ReservationList)
        {
            int[] popularRoomTypeArr = { 0, 0, 0, 0 };

            foreach (var res in ReservationList)
            {
                string roomType = (string)res.GetReservation()["roomType"];

                switch (roomType)
                {
                    case "Double":
                        int doubleRoom = popularRoomTypeArr[0] + 1;
                        popularRoomTypeArr[0] = doubleRoom;
                        break;
                    case "Twin":
                        int TwinRoom = popularRoomTypeArr[1] + 1;
                        popularRoomTypeArr[1] = TwinRoom;
                        break;
                    case "Family":
                        int FamilyRoom = popularRoomTypeArr[2] + 1;
                        popularRoomTypeArr[2] = FamilyRoom;
                        break;
                    case "Suite":
                        int SuiteRoom = popularRoomTypeArr[3] + 1;
                        popularRoomTypeArr[3] = SuiteRoom;
                        break;
                    default:
                        throw new NullReferenceException("Room Type cannot be found");
                }
            }

            return popularRoomTypeArr;
        }

        string[] IAnalyticsStrategy.GenerateChartXAxis()
        {
            String[] roomTypes = { "Double", "Twin", "Family", "Suite" };
            return roomTypes;
        }
    }
}
