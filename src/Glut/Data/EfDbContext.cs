using Glut.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Glut.Data
{
    public class EfDbContext : DbContext
    {
        ///public EfDbContext() { }

        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<GlutProject> Projects { get; set; }
        
        public DbSet<GlutResultItem> Results { get; set; }

        public DbSet<GlutRunAttribute> RunAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlite(@"Data Source=C:\Temp\Glut\Glut.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GlutProjectConfiguration());
            modelBuilder.ApplyConfiguration(new GlutResultItemConfiguration());
            modelBuilder.ApplyConfiguration(new GlutRunAttributeConfiguration());
        }
    }
}
