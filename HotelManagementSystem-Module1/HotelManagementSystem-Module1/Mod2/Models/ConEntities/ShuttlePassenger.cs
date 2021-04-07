using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConEntities
{
    /*
    * Author: Mod 2 Team 2
    * ShuttlePassenger Entity Class
    */
    public class ShuttlePassenger
    {

        //this class represents a passenger that is booked for a shuttle
        //used to account for seating capacity of buses.

        [Key]
        private string Id { get; set; }  
        private DateTime TransactionDateTime { get; set; }   //i.e. creation date
        private string ShuttleScheduleId { get; set; }   //id of the ShuttleService that this passenger is under
        private string ShuttleBusId { get; set; }        //id of the ShuttleBus that the passenger will take
        private string PassengerIndex { get; set; }      //index number of the passenger. NOT the guestId. this uniquely identifies the passenger within the bus+ schedule
        //note: PassengerIndex may be redundant.    

        protected ShuttlePassenger() { }

        public ShuttlePassenger(string id, DateTime creationDateTime, string shuttleScheduleID, string shuttleBusID, string passengerNo)
        {
            Id = id;
            TransactionDateTime = creationDateTime;
            ShuttleScheduleId = shuttleScheduleID;
            ShuttleBusId = shuttleBusID;
            PassengerIndex = passengerNo;
        }

        public void SetShuttlePassenger(string id, DateTime creationDateTime, string shuttleScheduleID, string shuttleBusID, string passengerNo)
        {
            Id = id;
            TransactionDateTime = creationDateTime;
            ShuttleScheduleId = shuttleScheduleID;
            ShuttleBusId = shuttleBusID;
            PassengerIndex = passengerNo;
        }

        public string RetrieveId()
        {
            return Id;
        }
        public string RetrieveShuttleBusId()
        {
            return ShuttleBusId;
        }
        public string RetrieveShuttleScheduleId()
        {
            return ShuttleScheduleId;
        }

        public DateTime RetrieveTransactionDateTime()
        {
            return TransactionDateTime;
        }
        public void SetShuttlePassenger(string id, string shuttleScheduleID, string shuttleBusID, string passengerNo)
        {
            Id = id;
            ShuttleScheduleId = shuttleScheduleID;
            ShuttleBusId = shuttleBusID;
            PassengerIndex = passengerNo;
        }

        public class ReadOnly
        {
            public string Id { get; set; }
            public DateTime TransactionDateTime { get; set; }
            public string ShuttleScheduleId { get; set; }
            public string ShuttleBusId { get; set; }
            public string PassengerIndex { get; set; }
        }

        public ReadOnly RetrieveShuttlePassengerObject()
        {
            ReadOnly returnObject = new ReadOnly
            {
                Id = Id,
                TransactionDateTime = TransactionDateTime,
                ShuttleScheduleId = ShuttleScheduleId,
                ShuttleBusId = ShuttleBusId,
                PassengerIndex = PassengerIndex
            };

            return returnObject;
        }

    }
}
