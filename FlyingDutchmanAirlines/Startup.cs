using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Repositories;
using FlyingDutchmanAirlines.ServiceLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FlyingDutchmanAirlines
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<FlightService>();
            services.AddTransient<FlightRepository>();
            services.AddTransient<AirportRepository>();

            services.AddTransient<BookingService>();
            services.AddTransient<BookingRepository>();
            services.AddTransient<CustomerRepository>();
            //services.AddTransient<FlyingDutchmanAirlinesContext>();

            services.AddDbContext<FlyingDutchmanAirlinesContext>(opt =>
            {
                //var connString = Environment.GetEnvironmentVariable("Sqlite_Connection") ?? string.Empty;
                var connString = "Data Source = FlyingDtuchmanV1.db";
                opt.UseSqlite(connString);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            
            app.UseRouting();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(swagger => swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Flying Dutchman Airlines"));

        }
    }
}