using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UzWorks.Core.Entities.JobAndWork;
using UzWorks.Core.Entities.Location;

namespace UzWorks.Persistence.Data;

public class UzWorksDbContext : DbContext
{
    public UzWorksDbContext(DbContextOptions<UzWorksDbContext> options) : base(options) { }

    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<JobCategory> JobCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
