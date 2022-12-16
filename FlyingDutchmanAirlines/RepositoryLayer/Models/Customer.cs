using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}