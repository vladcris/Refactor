using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using FlyingDutchmanAirlines_Tests.Stubs;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines_Tests.TestResults
{
    [TestClass]
    public class BookingRepositoryTests
    {
        private FlyingDutchmanAirlinesContext? _context;
        private FlyingDutchmanAirlinesContext? _contextStub;
        private BookingRepository? _repository;
        private BookingRepository? _repositoryStub;

        [TestInitialize] //runs for each test
        public void TestInitialize()
        {
            DbContextOptions<FlyingDutchmanAirlinesContext> dbOptions = new 
            DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
            .UseInMemoryDatabase("Filename=:memory:").Options;
            _context = new FlyingDutchmanAirlinesContext(dbOptions);
            _contextStub = new FlyingDutchmanAirlinesContext_Stub(dbOptions);

            //await _context.SaveChangesAsync();

            _repository = new BookingRepository(_context);
            _repositoryStub = new BookingRepository(_contextStub);
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        public async Task CreateBooking_Successs()
        {
            await _repositoryStub!.CreateBooking(1,1);
            Booking booking = _contextStub!.Bokings.First();
            
            Assert.IsNotNull(booking);
            Assert.AreEqual(1, booking.CustomerId);
            Assert.AreEqual(1, booking.FlightNumberId);
        }

        [TestMethod]
        [DataRow(-1,0)]
        [DataRow(1,0)]
        [DataRow(0,1)]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateBooking_Failure_InvalidInputs(int customerId, int flightNumber)
        {
            await _repository!.CreateBooking(customerId, flightNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(CouldNotAddBookingToDatabaseException))]
        public async Task CreateBooking_Failure_DatabaseError()
        {
            await _repositoryStub!.CreateBooking(2,1);
        }
    }
}