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
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            _repository = new FlightRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        [DataRow(1,1,2)]
        public async Task GetFlight_Success(int flightNumber, int origin, int destination)
        {
            var flight = await _repository!.GetFlightByFlightNumber(flightNumber, origin, destination);

            Assert.IsNotNull(flight);
            Assert.AreEqual(1, flight.FlightNumber);
            Assert.AreEqual(1, flight.Origin);
            Assert.AreEqual(2, flight.Destination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByFlightNumber_Failure_InvalidOrigin()
        {
            await _repository.GetFlightByFlightNumber(1,-1,1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlightByFlightNumber_Failure_InvalidDestination()
        {
            await _repository.GetFlightByFlightNumber(1,1,-1);
        }
    }
}