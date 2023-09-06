// ElevatorEngine.ConsoleApp/Program.cs

using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Application.Mapper;
using ElevatorEngine.Application.Services;
using ElevatorEngine.ConsoleApp.Utilities;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace ElevatorEngine.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var services = host.Services;


            var elevatorService = services.GetRequiredService<IElevatorService>();
            var floorService = services.GetRequiredService<IFloorService>();

            // Interact with services as required for the simulation.

            // In Main method of Program.cs
            var simulationManager = services.GetRequiredService<SimulationManager>();
            simulationManager.Start();


            host.Run();
        }

     public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(hostContext.Configuration.GetConnectionString("ElevatorConnection")));
            services.AddAutoMapper(typeof(Program));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddTransient<IFloorRepository, FloorRepository>();
            services.AddTransient<IElevatorRepository, ElevatorRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IElevatorService, ElevatorService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddTransient<IElevatorOrchestratorService, ElevatorOrchestratorService>();
            services.AddScoped<IEventPublishService, EventPublisher>();
            services.AddSingleton<SimulationManager>();
        });
    }
}
