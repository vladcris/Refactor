using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.RepositoryLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class FlyingDutchmanAirlinesContext : DbContext
    {
        public FlyingDutchmanAirlinesContext()
        {
            
        }

        public FlyingDutchmanAirlinesContext(DbContextOptions
        <FlyingDutchmanAirlinesContext> opt) : base(opt)
        {
            
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bokings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var path = Path.Combine( new string[] { Directory.GetCurrentDirectory(), "FlyingDtuchmanV1.db" } );
            optionsBuilder.UseSqlite($"Filename=C:\\Users\\const\\source\\repos\\CodeLikeAProInC#\\Refactor\\FlyingDutchmanAirlines\\FlyingDtuchmanV1.db");
            //optionsBuilder.UseSqlite($"Filename=:memory:");
        }
    }
}