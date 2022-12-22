using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Models;

namespace FlyingDutchmanAirlines.RepositoryLayer.Repositories
{
    public class BookingRepository
    {
        private readonly FlyingDutchmanAirlinesContext _dbContext;

        public BookingRepository(FlyingDutchmanAirlinesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateBooking(int customerId, int flightNumber)
        {
            if(customerId <= 0 || flightNumber <= 0)
                throw new ArgumentException("Invalid arguments provided");
            
            Booking newBooking = new Booking{
                CustomerId = customerId,
                FlightNumberId = flightNumber
            };

            try
            {
                _dbContext.Bokings.Add(newBooking);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new CouldNotAddBookingToDatabaseException();
            }
            
        }
    }
}