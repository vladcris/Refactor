using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.Views;
using Microsoft.AspNetCore.Mvc;

namespace FlyingDutchmanAirlines.ControllerLayer
{
    [Route("[controller]")]
    public class FlightController : Controller
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            try
            {
                Queue<FlightView> flights = new Queue<FlightView>();
                await foreach (FlightView flight in _flightService.GetFlights())
                {
                    flights.Enqueue(flight);
                }
                return StatusCode((int)HttpStatusCode.OK, flights);
            }
            catch (FlightNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "No flights were found");
            }
            catch(Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Server Error");
            }
        }

        [HttpGet("{flightNumber}")]
        public async Task<IActionResult> GetFlightByFightNumber(int flightNumber)
        {
            try
            {
                if(flightNumber <= 0) {
                    throw new Exception();
                }

                FlightView flight = await _flightService.GetFlightByFlightNumber(flightNumber);
                return StatusCode((int)HttpStatusCode.OK, flight);
            }
            catch (FlightNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.NotFound, "Flight not found");
            }
            catch(Exception)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Bad Request");
            }
        }
    }
}