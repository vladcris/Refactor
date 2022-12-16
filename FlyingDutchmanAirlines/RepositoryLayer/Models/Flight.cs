using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer.Models
{
    public class Flight
    {
        [Key]
        public int FlightNumber { get; set; }
        [ForeignKey("Origin")]
        public int Origin { get; set; }
        public Airport AirportOrigin { get; set; }

        [ForeignKey("Destination")]
        public int Destination { get; set; }
        public Airport AirportDestination { get; set; }


    }
}