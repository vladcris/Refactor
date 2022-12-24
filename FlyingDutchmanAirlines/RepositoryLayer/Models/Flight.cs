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
        [ForeignKey(nameof(Airport))]
        public int Origin { get; set; }

        [ForeignKey(nameof(Airport))]
        public int Destination { get; set; }
   

    }
}