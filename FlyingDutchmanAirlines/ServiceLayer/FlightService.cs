using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using FlyingDutchmanAirlines.Views;

namespace FlyingDutchmanAirlines.ServiceLayer
{
    public class FlightService
    {
        private readonly FlightRepository _flightRepository;
        private readonly AirportRepository _airportRepository;

        public FlightService(FlightRepository flightRepository, AirportRepository airportRepository)
        {
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
        }

        public virtual async IAsyncEnumerable<FlightView> GetFlights()
        {
            Queue<Flight> flights = _flightRepository.GetFlights();
            Airport originAirport;
            Airport destinationAirport;

            foreach (var flight in flights)
            {
                
                originAirport = await GetAirportInfo(flight.Origin);
                destinationAirport = await GetAirportInfo(flight.Destination);
                
                FlightView view = new FlightView(flight.FlightNumber.ToString(),
                                                 (originAirport.City, originAirport.IATA),
                                                 (destinationAirport.City, destinationAirport.IATA));
                
                yield return view;
            }
        }

        public virtual async Task<FlightView> GetFlightByFlightNumber(int flightNumber)
        {
            Airport originAirport;
            Airport destinationAirport;

            try
            {
                Flight flight = await _flightRepository.GetFlightByFlightNumber(flightNumber);
                originAirport = await GetAirportInfo(flight.Origin);
                destinationAirport = await GetAirportInfo(flight.Destination);

                return new FlightView(flight.FlightNumber.ToString(),
                                     (originAirport.City, originAirport.IATA),
                                     (destinationAirport.City, destinationAirport.IATA));

            }
            catch (FlightNotFoundException)
            {
                throw new FlightNotFoundException();
            }
            catch(Exception)
            {
                throw new ArgumentException();
            }
        }

        private async Task<Airport> GetAirportInfo(int airportNumber)
        {
            try
            {
                return await _airportRepository.GetAirportById(airportNumber);
            }
            catch (FlightNotFoundException)
            {
                throw new FlightNotFoundException();
            }
            catch(Exception)
            {
                throw new ArgumentException();
            }
        }
        
    }
}