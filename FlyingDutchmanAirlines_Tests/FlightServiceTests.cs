using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FlyingDutchmanAirlines_Tests
{
    [TestClass]
    public class FlightServiceTests
    {

        private Mock<AirportRepository>? airportRepo;
        private Mock<FlightRepository>? flightRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            airportRepo = new Mock<AirportRepository>();
            flightRepo = new Mock<FlightRepository>();

            Flight flight = new Flight{
                FlightNumber = 148, 
                Origin = 31, 
                Destination = 92 
            };

            Queue<Flight> mockReturn = new Queue<Flight>(1); 
            mockReturn.Enqueue(flight); 

            flightRepo!.Setup(repo => repo.GetFlights()).Returns(mockReturn);
            airportRepo!.Setup(repo => repo.GetAirportById(31)).ReturnsAsync(new Airport{
                AirportId = 31,
                City = "Mexico City",
                IATA = "MEX"
            });

            airportRepo!.Setup(repo => repo.GetAirportById(92)).ReturnsAsync(new Airport{
                AirportId = 92,
                City = "Ulaaan",
                IATA = "UBN"
            });

        }

        [TestMethod]
        public async Task GetFlights_Success()
        {
            
            FlightService service = new FlightService(flightRepo!.Object, airportRepo!.Object);

            await foreach (var flightView in service.GetFlights())
            {
                Assert.IsNotNull(flightView); 
                Assert.AreEqual(flightView.FlightNumber, "148"); 
                Assert.AreEqual(flightView.Origin.City, "Mexico City"); 
                Assert.AreEqual(flightView.Origin.Code, "MEX"); 
                Assert.AreEqual(flightView.Destination.City, "Ulaaan"); 
                Assert.AreEqual(flightView.Destination.Code, "UBN"); 
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FlightNotFoundException))]
        public async Task GetFlights_Failure_RepositoryException()
        {
            airportRepo!.Setup(repo => repo.GetAirportById(31)).Throws(new FlightNotFoundException());

            FlightService service = new FlightService(flightRepo!.Object, airportRepo!.Object);

            await foreach (var _ in service.GetFlights())
            {
                ;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetFlights_Failure_RegularException()
        {
            airportRepo!.Setup(repo => repo.GetAirportById(31)).Throws(new NullReferenceException());

            FlightService service = new FlightService(flightRepo!.Object, airportRepo!.Object);

            await foreach (var _ in service.GetFlights())
            {
                ;
            }
        }

        [TestMethod]
        public async Task GetFlightByNumber_Success()
        {
            flightRepo!.Setup(repo => repo.GetFlightByFlightNumber(148)).ReturnsAsync(new Flight{
                FlightNumber = 148,
                Origin = 31, 
                Destination = 92 
            });

            FlightService service = new FlightService(flightRepo!.Object, airportRepo!.Object);

           var flight =  await service.GetFlightByFlightNumber(148);

           Assert.IsInstanceOfType(flight, typeof(FlightView));
           Assert.AreEqual(flight.Origin.City, "Mexico City");

        }

        [TestMethod]
        [ExpectedException(typeof(FlightNotFoundException))]
        public async Task GetFlightByNumber_Failure_RepositoryException()
        {
            flightRepo!.Setup(repo => repo.GetFlightByFlightNumber(-1)).ThrowsAsync(new FlightNotFoundException());
            
            FlightService service = new FlightService(flightRepo!.Object, airportRepo!.Object);
            await service.GetFlightByFlightNumber(-1);
        }
    }
}