using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.ControllerLayer;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FlyingDutchmanAirlines_Tests.ControllerLayer
{
    [TestClass]
    public class FlightControllerTests
    {
        private Mock<FlightService>? _flightService;
        private Queue<FlightView>? _mockData;

        [TestInitialize]
        public void TestInitialize()
        {
            _flightService = new Mock<FlightService>();
                
        }

        [TestMethod]
        public async Task GetFlights_Success()
        {
           FlightController controller = new FlightController(_flightService!.Object);
        
            FlightView flight = new FlightView("22", ("Arges", "AG"), ("Bucuresti", "BUC"));
            _mockData =  new Queue<FlightView>(1);
            _mockData.Enqueue(flight);

            _flightService.Setup(service => service.GetFlights()).Returns(FlightViewAsyncGenerator(_mockData));


           ObjectResult response = await controller.GetFlights() as ObjectResult ?? new ObjectResult(null); 
           Queue<FlightView> content = response.Value as Queue<FlightView> ?? new Queue<FlightView>();

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);

            Assert.IsTrue(_mockData.All(flight => content.Contains(flight)));
        }

        [TestMethod]
        //[ExpectedException(typeof(FlightNotFoundException))]
        public async Task GetFlights()
        {
            _flightService!.Setup(service => service.GetFlights()).Throws(new FlightNotFoundException());
            var controller = new FlightController(_flightService.Object);
            
            ObjectResult response =  await controller.GetFlights() as ObjectResult ?? new ObjectResult(null);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("No flights were found", response.Value);
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Success()
        {
            FlightView flight = new FlightView("22", ("Arges", "AG"), ("Bucuresti", "BUC"));
            _flightService!.Setup(s => s.GetFlightByFlightNumber(22)).ReturnsAsync(flight);

            var controller = new FlightController(_flightService.Object);
            ObjectResult response = await controller.GetFlightByFightNumber(22) as ObjectResult ?? new ObjectResult(null);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Failure_ServiceException()
        {
            _flightService!.Setup(s => s.GetFlightByFlightNumber(22)).ThrowsAsync(new FlightNotFoundException());

            var controller = new FlightController(_flightService.Object);
            ObjectResult response = await controller.GetFlightByFightNumber(22) as ObjectResult ?? new ObjectResult(null);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task GetFlightByFlightNumber_Failure_ReqularException()
        {
            _flightService!.Setup(s => s.GetFlightByFlightNumber(22)).ThrowsAsync(new OverflowException());

            var controller = new FlightController(_flightService.Object);
            ObjectResult response = await controller.GetFlightByFightNumber(22) as ObjectResult ?? new ObjectResult(null);

            Assert.IsNotNull(response);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        private async IAsyncEnumerable<FlightView> FlightViewAsyncGenerator(IEnumerable<FlightView> views)
        {
            foreach (var item in views)
            {
                yield return item;
            }
        }

    }
}