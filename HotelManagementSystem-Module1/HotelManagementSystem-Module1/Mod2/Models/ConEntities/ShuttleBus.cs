using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.ConEntities
{
    /*
    * Author: Mod 2 Team 2
    * ShuttleBus Entity Class
    */
    public class ShuttleBus
    {
        //this class represents a physical shuttle bus - project spec says there will be 5 of this
        //used to account for seating capacity of buses by tallying with ShuttlePassenger entities related to a specific bus's ID

        [Key]
        private string Id { get; set; }  //represents bus ID
        private DateTime RequisitionDateTime { get; set; }   //i.e. creation date
        private string BusLabel { get; set; }       // label for physically identification of bus. not meant to be used as key.
        private int NumberOfSeats { get; set; }     //used to tally amt of seats. project spec is 12 but new buses may be different. 

        protected ShuttleBus() { }

        public ShuttleBus(string id, string busLabel, int numOfSeats)
        {
            Id = id;
            RequisitionDateTime = DateTime.Now;
            BusLabel = busLabel;
            NumberOfSeats = numOfSeats;
        }

        public void SetShuttleBus(string id, string busLabel, int numOfSeats)
        {
            Id = id;
            RequisitionDateTime = DateTime.Now;
            BusLabel = busLabel;
            NumberOfSeats = numOfSeats;
        }

        
        public string RetrieveId()
        {
            return Id;
        }

        /// <summary>
        /// returns the NumberOfSeats value, that is, the maximum amount of passengers this shuttle can take
        /// NOTE: does not return the 'CURRENT' amount of passengers. that value should be derived with ShuttlePassenger
        /// </summary>
        public int RetrieveShuttleBusCapacity()
        {
            return NumberOfSeats;
        }

        public class ReadOnly
        {
            public string Id { get; set; } 
            public DateTime RequisitionDateTime { get; set; }
            public string BusLabel { get; set; }
            public int NumberOfSeats { get; set; }
        }

        public ReadOnly RetrieveShuttleBusObject()
        {
            ReadOnly returnObject = new ReadOnly
            {
                Id = Id,
                RequisitionDateTime = RequisitionDateTime,
                BusLabel = BusLabel,
                NumberOfSeats = NumberOfSeats
            };

            return returnObject;
        }
    }
}
