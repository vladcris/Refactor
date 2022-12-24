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
    public class AirportRepository
    {
        private readonly FlyingDutchmanAirlinesContext _dbContext;

        public AirportRepository()
        {
            if(Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
                throw new Exception("this constructor should only be used for testing");
        }
        public AirportRepository(FlyingDutchmanAirlinesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<Airport> GetAirportById(int airportId)
        {
            if(airportId <= 0)
                throw new ArgumentException("Invalid argument");

            return await _dbContext.Airports.FirstOrDefaultAsync(a => a.AirportId == airportId)
                         ?? throw new AirportNotFoundException();
        }
    }
}