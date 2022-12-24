using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.Views
{
    public class FlightView
    {
        public string FlightNumber { get; private set; }
        public AirportInfo Origin { get; private set; }
        public AirportInfo Destination { get; private set; }

        public FlightView(string flightNumber, (string city, string code) origin, (string city, string code) destination)
        {
            FlightNumber = string.IsNullOrEmpty(flightNumber) ? "No flight number found" : flightNumber;
            Origin = new AirportInfo(origin);
            Destination = new AirportInfo(destination);
        }
    }
} 