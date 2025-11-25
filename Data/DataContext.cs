using csharp_bus_watcher_api.Models;
using Microsoft.EntityFrameworkCore;

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

    public DbSet<BusRoute> Routes { get; set; }

    public DbSet<Stop> Stops { get; set; }

    public DbSet<Area> Areas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Device -> DeviceBus (One-to-Many)
        modelBuilder.Entity<DeviceBus>()
        .HasOne(db => db.Device)
        //.WithMany(d => d.DeviceBuses)
        .WithMany()
        .HasForeignKey(db => db.DeviceId)
        .OnDelete(DeleteBehavior.Restrict); // Prevent device deletion if linked to buses

        // Bus -> DeviceBus (One-to-Many)
        modelBuilder.Entity<DeviceBus>()
            .HasOne(db => db.Bus)
            //.WithMany(b => b.DeviceBuses)
            .WithMany()
            .HasForeignKey(db => db.BusId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent bus deletion if linked to devices

        // Bus -> Route (Many-to-One)
        modelBuilder.Entity<Bus>()
            .HasOne(b => b.Route)
            //.WithMany(r => r.Buses)
            .WithMany()
            .HasForeignKey(b => b.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Route -> OriginStop (Many-to-One)
        modelBuilder.Entity<BusRoute>()
            .HasOne(r => r.OriginStop)
            //.WithMany(s => s.RoutesAsOrigin)
            .WithMany()
            .HasForeignKey(r => r.OriginStopId)
            .OnDelete(DeleteBehavior.Restrict);

        // Route -> DestinationStop (Many-to-One)  
        modelBuilder.Entity<BusRoute>()
            .HasOne(r => r.DestinationStop)
            //.WithMany(s => s.RoutesAsDestination)
            .WithMany()
            .HasForeignKey(r => r.DestinationStopId)
            .OnDelete(DeleteBehavior.Restrict);

        // Stop -> Area (Many-to-One)
        modelBuilder.Entity<Stop>()
            .HasOne(b => b.Area)
            //.WithMany(r => r.Stops)
            .WithMany()
            .HasForeignKey(b => b.AreaId)
            .OnDelete(DeleteBehavior.Restrict);

        // IncidentReport -> IncidentReportFeedback (One-to-Many)
        modelBuilder.Entity<IncidentReportFeedback>()
            .HasOne(db => db.IncidentReport)
            //.WithMany(d => d.IncidentReportFeedbacks)
            .WithMany()
            .HasForeignKey(db => db.IncidentReportId)
            .OnDelete(DeleteBehavior.Restrict);

        // Device -> IncidentReport (One-to-Many)
        modelBuilder.Entity<IncidentReport>()
            .HasOne(ir => ir.CreatedByDevice)
            //.WithMany(d => d.IncidentReports)
            .WithMany()
            .HasForeignKey(ir => ir.CreatedByDeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Device -> IncidentReportFeedback (One-to-Many)
        modelBuilder.Entity<IncidentReportFeedback>()
            .HasOne(irf => irf.CreatedByDevice)
            //.WithMany(d => d.IncidentReportFeedbacks)
            .WithMany()
            .HasForeignKey(irf => irf.CreatedByDeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Bus -> IncidentReport (One-to-Many)
        modelBuilder.Entity<IncidentReport>()
            .HasOne(ir => ir.Bus)
            //.WithMany(b => b.IncidentReports)
            .WithMany()
            .HasForeignKey(ir => ir.BusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IncidentReport>()
            .Ignore(ir => ir.CreatedByDevice); // dont populate CreatedByDevice

        base.OnModelCreating(modelBuilder);
    }
}