using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer.Models
{
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }
        public string City { get; set; }
        public string IATA { get; set; }
    }
}