using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines_Tests.TestResults
{
    [TestClass]
    public class AirportRepositoryTests
    {
        private FlyingDutchmanAirlinesContext? _context;
        private AirportRepository? _repository;

        [TestInitialize]
        public async Task TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dboptions = new 
            DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
            .UseInMemoryDatabase("Filename=:memory:").Options;
            _context = new FlyingDutchmanAirlinesContext(dboptions);

            var airport = new Airport{
                AirportId = 1,
                City = "Nuuk",
                IATA = "GOH"
            };

            SortedList<string, Airport> airports = new SortedList<string, Airport>
            {
                {"GOH", new Airport{AirportId=1, City="Nuuk", IATA="GOH"} },
                {"PHX", new Airport{AirportId=2, City="Pheonix", IATA="PHX"} },
                {"DDH", new Airport{AirportId=3, City="Bennington", IATA="DDH"} },
                {"RDU", new Airport{AirportId=4, City="Durham", IATA="RDU"} }

            };
            //_context.Airports.Add(airport);
            _context.Airports.AddRange(airports.Values);
            await _context.SaveChangesAsync();

            _repository = new AirportRepository(_context);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task GetAirportById_Success()
        {
            Airport airport = await _repository.GetAirportById(1);

            Assert.IsNotNull(airport);
            Assert.AreEqual(1, airport.AirportId);
            Assert.AreEqual("Nuuk", airport.City);
            Assert.AreEqual("GOH", airport.IATA);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetAirportById_Failure_InvalidInput()
        {
            using ( StringWriter outputStream = new StringWriter())
            {
                Console.SetOut(outputStream);
                await _repository!.GetAirportById(-1);

                Assert.IsTrue(outputStream.ToString().Contains("Invalid argument"));
            }
            //Airport airport = await _repository.GetAirportById(-1);
        }



    }
}