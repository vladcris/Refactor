using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;

namespace FlyingDutchmanAirlines.ServiceLayer
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly FlightRepository _flightRepository;

        public BookingService(BookingRepository bookingRepo, CustomerRepository customerRepository, FlightRepository flightRepository)
        {
            _bookingRepository = bookingRepo;
            _customerRepository = customerRepository;
            _flightRepository = flightRepository;
        }

        public async Task<(bool, Exception)> CreateBooking(string customerName, int flightNumber)
        {

            if(flightNumber <= 0 || string.IsNullOrEmpty(customerName))
                return  (false, new ArgumentException("Invalid arguments provided"));          
            try
            {
                if(!await FlightExistsInDatabase(flightNumber))
                    throw new CouldNotAddBookingToDatabaseException();

                Customer customer = await GetCustomerFromDatabase(customerName) ?? 
                                await AddCustomerToDatabase(customerName);
                
                await _bookingRepository.CreateBooking(customer.CustomerId, flightNumber);
                return (true, null);

            }
            catch (Exception ex)
            {
                return (false, ex);
            }
        }

        private async Task<Customer> AddCustomerToDatabase(string name)
        {
            await _customerRepository.CreateCustomer(name);
            return await _customerRepository.GetCustomerByName(name);
        }

        private async Task<Customer> GetCustomerFromDatabase(string name)
        {
            try
            {
                return await _customerRepository.GetCustomerByName(name);
            }
            catch (CustomerNotFoundException)
            {
                return null;
            }
            catch(Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException ?? new Exception()).Throw();
                return null;
            }
        }

        private async Task<bool> FlightExistsInDatabase(int flightNumber)
        {
            try
            {
                return await _flightRepository.GetFlightByFlightNumber(flightNumber) != null;
            }
            catch (FlightNotFoundException)
            {
                return false;
            }
        }
    
    }
}