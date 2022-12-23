using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using FlyingDutchmanAirlines.ServiceLayer;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FlyingDutchmanAirlines_Tests
{
    [TestClass]
    public class BookingServiceTests
    {
        private Mock<BookingRepository>? bookingRepo;
        private Mock<CustomerRepository>? customerRepo;
        private Mock<FlightRepository>? flightRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            bookingRepo = new Mock<BookingRepository>();
            customerRepo = new Mock<CustomerRepository>();
            flightRepo = new Mock<FlightRepository>();
        }

        [TestMethod]
        public async Task CreateBooking_Success()
        {

            bookingRepo!.Setup(repo => repo.CreateBooking(1, 1)).Returns(Task.CompletedTask);
            customerRepo!.Setup(repo => repo.GetCustomerByName("Leo Tolstoy"))
                .Returns(Task.FromResult(new Customer("Leo Tolstoy")));
            flightRepo!.Setup(repo => repo.GetFlightByFlightNumber(1)).Returns(Task.FromResult(new Flight()));

            BookingService service = new BookingService(bookingRepo.Object, customerRepo.Object, flightRepo!.Object);

            (bool result, Exception exception) = await service.CreateBooking("Leo Tolstoy", 1);

            Assert.IsTrue(result);
            Assert.IsNull(exception);
        }

        [TestMethod]
        [DataRow("", 0)]
        [DataRow(null, -1)]
        [DataRow("Test", -1)]
        public async Task CreateBooking_Failure_InvalidInputs(string name, int flightnNumber)
        {
            BookingService service = new BookingService(bookingRepo!.Object, customerRepo!.Object, flightRepo!.Object);

            (bool result, Exception exception) = await service.CreateBooking(name, flightnNumber);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task CreateBooking_Failure_RepositoryException()
        {
            bookingRepo!.Setup(repo => repo.CreateBooking(1, 1)).Throws(new ArgumentException());
            bookingRepo!.Setup(repo => repo.CreateBooking(2, 2)).Throws(new CouldNotAddBookingToDatabaseException());

            customerRepo!.Setup(repo => repo.GetCustomerByName("Galileo"))
                .Returns(Task.FromResult(new Customer("Galileo") {CustomerId = 1}));
            customerRepo.Setup(repo => repo.GetCustomerByName("John"))
                .Returns(Task.FromResult(new Customer("John") {CustomerId = 2}));
            flightRepo!.Setup(repo => repo.GetFlightByFlightNumber(1)).ReturnsAsync(new Flight());
            flightRepo!.Setup(repo => repo.GetFlightByFlightNumber(2)).ReturnsAsync(new Flight());

            BookingService service = new BookingService(bookingRepo.Object, customerRepo.Object, flightRepo!.Object);

            (bool result, Exception exception) = await service.CreateBooking("Galileo", 1);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentException));

            (result, exception) = await service.CreateBooking("John", 2);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));
        }


        [TestMethod]
        public async Task CreateBooking_Failure_FlightNotInDatabase()
        {
            flightRepo!.Setup(repo => repo.GetFlightByFlightNumber(5)).Throws(new FlightNotFoundException());

            BookingService service = new BookingService(bookingRepo!.Object, customerRepo!.Object, flightRepo!.Object); 

            (bool result, Exception exception) = await service.CreateBooking("test", 5);

            Assert.IsFalse(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));

        }
    }
}