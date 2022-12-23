using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer.Repositories
{
    public class FlightRepository
    {
        private readonly FlyingDutchmanAirlinesContext _dbContext;

        public FlightRepository()
        {
            if(Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
                throw new Exception("this constructor should only be used for testing");
        }
        public FlightRepository(FlyingDutchmanAirlinesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Flight> GetFlightByFlightNumber(int flightNumber)
        {
            if(flightNumber <= 0)
                throw new ArgumentException($"Invalid arguments Flightnumber: {flightNumber}.");
            
            
            return await _dbContext.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber)
                        ?? throw new FlightNotFoundException();
        }
    }
}