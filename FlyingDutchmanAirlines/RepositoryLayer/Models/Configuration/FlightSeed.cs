using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlyingDutchmanAirlines.RepositoryLayer.Models.Configuration
{
    public class FlightSeed : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasData(
                new Flight
                {
                    FlightNumber = 1,
                    Origin = 1,
                    Destination = 2
                },
                new Flight
                {
                    FlightNumber = 2,
                    Origin = 2,
                    Destination = 1

                }
            );
        }
    }
}