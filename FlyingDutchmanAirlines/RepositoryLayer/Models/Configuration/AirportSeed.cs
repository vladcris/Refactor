using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlyingDutchmanAirlines.RepositoryLayer.Models.Configuration
{
    public class AirportSeed : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.HasData(
                new Airport
                {
                    AirportId = 1,
                    City = "Groningen",
                    IATA = "GRQ"
                },
                new Airport
                {
                    AirportId = 2,
                    City = "London",
                    IATA = "LHR"
                }
            );
        }
    }
}