using ElevatorEngine.Application.Interfaces;
using ElevatorEngine.Application.Mapper;
using ElevatorEngine.Application.Services;
using ElevatorEngine.Domain.Interfaces;
using ElevatorEngine.Infrastructure.Data;
using ElevatorEngine.Infrastructure.EventHub;
using Microsoft.EntityFrameworkCore;
 
internal class Startup
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder.Services, builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        ConfigureApp(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSignalR();
        services.AddAutoMapper(typeof(Startup));
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddLogging();

        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ElevatorConnection")));

        // Register  repositories
        services.AddTransient<IElevatorRepository, ElevatorRepository>();
        services.AddTransient<IFloorRepository, FloorRepository>();
        services.AddTransient<IMaintenanceRepository, MaintenanceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register services
        services.AddScoped<IEventPublishService, EventPublisher>();
        services.AddScoped<IElevatorService, ElevatorService>();
        services.AddScoped<IFloorService, FloorService>();
        services.AddScoped<IMaintenanceService, MaintenanceService>();
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost:7256")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
       
    }

    private static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ApiExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ElevatorEventHub>("/elevatorEvents");
        });
        app.UseCors("CorsPolicy");
    }
}
