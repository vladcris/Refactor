using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer.Repositories
{
    public class FlightRepository
    {
        private readonly FlyingDutchmanAirlinesContext _dbContext;

        public FlightRepository(FlyingDutchmanAirlinesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int origin, int destination)
        {
            if(flightNumber <= 0 || origin <= 0 || destination <= 0)
                throw new ArgumentException($"Invalid arguments Flightnumber: {flightNumber}.");
            
            
            return await _dbContext.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber)
                        ?? throw new FlightNotFoundException();
        }
    }
}