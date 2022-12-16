using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.RepositoryLayer.Models;

namespace FlyingDutchmanAirlines.RepositoryLayer.Repositories
{
    public class CustomerRepository
    {
        private readonly FlyingDutchmanAirlinesContext _dbContext;
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

        private bool IsInvalidCustomerName(string name)
        {
            char[] forbiddenCharacters = {'!', '@', '#', '$', '%', '&', '*'};
            return  string.IsNullOrEmpty(name) || 
                    name.Any(x => forbiddenCharacters.Contains(x));
        }
    }
}