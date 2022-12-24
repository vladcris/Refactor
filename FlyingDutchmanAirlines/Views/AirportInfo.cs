using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.Views
{
    public struct AirportInfo
    {
        public string City { get; set; }
        public string Code { get; set; }

        public AirportInfo((string city, string code) airport)
        {
            City = string.IsNullOrEmpty(airport.city) ? "no city found" : airport.city;
            Code = string.IsNullOrEmpty(airport.code) ? "no code found" : airport.code;
        }
    }
}