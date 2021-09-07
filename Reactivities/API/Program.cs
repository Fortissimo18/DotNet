using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope(); //using: once the main method finished, scope will be disposed

            var services = scope.ServiceProvider;

            try{
                var context = services.GetRequiredService<DataContext>(); //the DataContext is configured in Startup.cs
                await context.Database.MigrateAsync(); //Applies any pending migrations for the context to the database. Will create the database if it does not already exist.
                await Seed.SeedData(context);
            }
            catch (Exception ex){
                var logger = services.GetRequiredService<ILogger<Program>>(); //<Program>: log that the error come from Program.cs
                logger.LogError(ex, "An error occurred during migration");
            }

            await host.RunAsync(); //start the application

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
