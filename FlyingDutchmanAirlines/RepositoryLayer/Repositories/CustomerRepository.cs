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
    public class CustomerRepository
    {
        private readonly FlyingDutchmanAirlinesContext _dbContext;
        public CustomerRepository()
        {
            if(Assembly.GetExecutingAssembly().FullName == Assembly.GetCallingAssembly().FullName)
                throw new Exception("this constructor should only be used for testing");
        }
        public CustomerRepository(FlyingDutchmanAirlinesContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateCustomer(string name)
        {
            if(IsInvalidCustomerName(name))
                return false;

            try
            {
                Customer newCustomer = new Customer(name);
                _dbContext.Customers.Add(newCustomer);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public virtual async Task<Customer> GetCustomerByName(string name)
        {
            if(IsInvalidCustomerName(name)) {
                throw new CustomerNotFoundException();
            }

            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Name == name) 
                    ?? throw new CustomerNotFoundException();
        }

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = {'!', '@', '#', '$', '%', '&', '*'};
            return  string.IsNullOrEmpty(name) || 
                    name.Any(x => forbiddenCharacters.Contains(x));
        }
    }
}