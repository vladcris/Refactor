using FlyingDutchmanAirlines;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

internal class Program
{
    internal static void Main(string[] args)
    {
        InitializeHost(args);
    }

    private static void InitializeHost(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
        {
            builder.UseUrls("http://0.0.0.0:8080")
                .UseStartup<Startup>();
        }).Build().Run();

    }

}