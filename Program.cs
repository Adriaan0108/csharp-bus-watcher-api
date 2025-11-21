using csharp_bus_watcher_api.Data;
using csharp_bus_watcher_api.Interfaces.Repositories;
using csharp_bus_watcher_api.Interfaces.Services;
using csharp_bus_watcher_api.Middleware;
using csharp_bus_watcher_api.Repositories;
using csharp_bus_watcher_api.Services;
using Expo.Server.Client;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<PushApiClient>();

builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceBusRepository, DeviceBusRepository>();
builder.Services.AddScoped<IIncidentReportRepository, IncidentReportRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();

builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IBusService, BusService>();
builder.Services.AddScoped<IDeviceContextService, DeviceContextService>();
builder.Services.AddScoped<IDeviceBusService, DeviceBusService>();
builder.Services.AddScoped<IIncidentReportService, IncidentReportService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRouteService, RouteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();