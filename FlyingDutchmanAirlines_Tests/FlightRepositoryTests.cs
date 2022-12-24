using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines_Tests
{
    [TestClass]
    public class FlightRepositoryTests
    {
        private FlyingDutchmanAirlinesContext? _context;
        private FlightRepository? _repository;

        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dboptions = new 
            DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
            .UseInMemoryDatabase("Filename=:memory:").Options;
            _context = new FlyingDutchmanAirlinesContext(dboptions);

            Flight flight = new Flight{
                FlightNumber = 1,
                Origin = 1,
                Destination = 2
            };
            Flight flight2 = new Flight{
                FlightNumber = 10,
                Origin = 3,
                Destination = 4
            };
            _context.Flights.Add(flight);
            _context.Flights.Add(flight2);
            await _context.SaveChangesAsync();

            _repository = new FlightRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        [DataRow(1,1,2)]
        public async Task GetFlight_Success(int flightNumber, int origin, int destination)
        {
            var flight = await _repository!.GetFlightByFlightNumber(flightNumber);

            Assert.IsNotNull(flight);
            Assert.AreEqual(1, flight.FlightNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByFlightNumber_Failure_InvalidOrigin()
        {
            await _repository!.GetFlightByFlightNumber(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByFlightNumber_Failure_InvalidDestination()
        {
            await _repository!.GetFlightByFlightNumber(1);
        }
    }
}