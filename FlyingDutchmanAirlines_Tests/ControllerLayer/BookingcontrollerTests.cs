using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.ControllerLayer.JsonData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingDutchmanAirlines_Tests.ControllerLayer
{
    [TestClass]
    public class BookingcontrollerTests
    {
        [TestMethod]
        public async Task CreateBooking_Success()
        {

        }

        [TestMethod]
        [DataRow("Vlad", null)]
        [DataRow("", "Cristina")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateBooking_Failure_InvalidInputData(string firstName, string lastName)
        {
            BookingData bookingData = new BookingData{
                FirstName = firstName,
                LastName = lastName
            };

        }
    }
}