using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.ControllerLayer.JsonData;
using FlyingDutchmanAirlines.ControllerLayer.ModelBinder;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace FlyingDutchmanAirlines.ControllerLayer
{
    [Route("[controller]")]
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("{flightNumber}")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingData bookingData, int flightNumber)
        {
            
            if(ModelState.IsValid && flightNumber > 0)
            {

                string name = $"{bookingData.FirstName} {bookingData.LastName}";
                (bool result, Exception exception) = await _bookingService.CreateBooking(name, flightNumber);

                if(result && exception == null)
                    return StatusCode((int)HttpStatusCode.Created);
                
                return exception is CouldNotAddBookingToDatabaseException 
                    ? StatusCode((int)HttpStatusCode.NotFound)
                    : StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, 
                                ModelState.Root.Errors.First().ErrorMessage);
        }
    }
}