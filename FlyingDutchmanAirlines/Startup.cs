using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlyingDutchmanAirlines
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // services.AddDbContext<FlyingDutchmanAirlinesContext>(opt =>
            // {
            //     //var connString = Environment.GetEnvironmentVariable("Sqlite_Connection") ?? string.Empty;
            //     var connString = "Data Source = FlyingDtuchmanV1.db";
            //     opt.UseSqlite(connString);
            // });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}