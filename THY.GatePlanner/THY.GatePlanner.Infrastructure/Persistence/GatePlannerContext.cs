using THY.GatePlanner.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace THY.GatePlanner.Infrastructure.Persistence;

public sealed class GatePlannerContext : DbContext
{
    public GatePlannerContext(DbContextOptions<GatePlannerContext> options): base(options)
    {
        if (Database.CanConnect()) return;

        if (!Database.EnsureCreated())
            throw new Exception("Database was not created!");

    }

    public DbSet<Gate> Gates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GateEntityConfiguration());
    }
}