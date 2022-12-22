using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.RepositoryLayer.Models.Internal;

namespace FlyingDutchmanAirlines.RepositoryLayer.Models
{
    public sealed class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public ICollection<Booking> Booking { get; set; }

        public Customer(string name)
        {
            Booking = new HashSet<Booking>();
            Name = name;
        }

        public static bool operator == (Customer x, Customer y)
        {
            CustomerEqualityComparer comparer = new CustomerEqualityComparer();
            return comparer.Equals(x, y);
        }

        public static bool operator != (Customer x, Customer y) => !(x == y);

    }
}