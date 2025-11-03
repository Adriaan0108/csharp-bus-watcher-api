using csharp_bus_watcher_api.Models;
using Microsoft.EntityFrameworkCore;
using Route = csharp_bus_watcher_api.Models.Route; // prevent ambiguous .NET naming

namespace csharp_bus_watcher_api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; }

    public DbSet<Bus> Buses { get; set; }

    public DbSet<DeviceBus> DeviceBuses { get; set; }

    public DbSet<IncidentReport> IncidentReports { get; set; }

    public DbSet<IncidentReportFeedback> IncidentReportFeedbacks { get; set; }

    public DbSet<Route> Routes { get; set; }

    public DbSet<Stop> Stops { get; set; }

    public DbSet<Area> Areas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Device -> DeviceBus (One-to-Many)
        modelBuilder.Entity<DeviceBus>()
            .HasOne(db => db.Device)
            .WithMany(d => d.DeviceBuses)
            .HasForeignKey(db => db.DeviceId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent device deletion if linked to buses

        // Bus -> DeviceBus (One-to-Many)
        modelBuilder.Entity<DeviceBus>()
            .HasOne(db => db.Bus)
            .WithMany(b => b.DeviceBuses)
            .HasForeignKey(db => db.BusId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent bus deletion if linked to devices

        // Bus -> Route (Many-to-One)
        modelBuilder.Entity<Bus>()
            .HasOne(b => b.Route)
            .WithMany(r => r.Buses)
            .HasForeignKey(b => b.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Route -> OriginStop (Many-to-One)
        modelBuilder.Entity<Route>()
            .HasOne(r => r.OriginStop)
            .WithMany(s => s.RoutesAsOrigin)
            .HasForeignKey(r => r.OriginStopId)
            .OnDelete(DeleteBehavior.Restrict);

        // Route -> DestinationStop (Many-to-One)  
        modelBuilder.Entity<Route>()
            .HasOne(r => r.DestinationStop)
            .WithMany(s => s.RoutesAsDestination)
            .HasForeignKey(r => r.DestinationStopId)
            .OnDelete(DeleteBehavior.Restrict);

        // Stop -> Area (Many-to-One)
        modelBuilder.Entity<Stop>()
            .HasOne(b => b.Area)
            .WithMany(r => r.Stops)
            .HasForeignKey(b => b.AreaId)
            .OnDelete(DeleteBehavior.Restrict);

        // IncidentReport -> IncidentReportFeedback (One-to-Many)
        modelBuilder.Entity<IncidentReportFeedback>()
            .HasOne(db => db.IncidentReport)
            .WithMany(d => d.IncidentReportFeedbacks)
            .HasForeignKey(db => db.IncidentReportId)
            .OnDelete(DeleteBehavior.Restrict);

        // Device -> IncidentReport (One-to-Many)
        modelBuilder.Entity<IncidentReport>()
            .HasOne(ir => ir.CreatedByDevice)
            .WithMany(d => d.IncidentReports)
            .HasForeignKey(ir => ir.CreatedByDeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Device -> IncidentReportFeedback (One-to-Many)
        modelBuilder.Entity<IncidentReportFeedback>()
            .HasOne(irf => irf.CreatedByDevice)
            .WithMany(d => d.IncidentReportFeedbacks)
            .HasForeignKey(irf => irf.CreatedByDeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Bus -> IncidentReport (One-to-Many)
        modelBuilder.Entity<IncidentReport>()
            .HasOne(ir => ir.Bus)
            .WithMany(b => b.IncidentReports)
            .HasForeignKey(ir => ir.BusId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}