using Microsoft.EntityFrameworkCore;


namespace Glut.Data
{
    public class EfDbContext : DbContext
    {
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
            modelBuilder.Entity<GlutProject>().ToTable(nameof(GlutProject));
            modelBuilder.Entity<GlutResultItem>().ToTable(nameof(GlutResultItem));
            modelBuilder.Entity<GlutRunAttribute>().ToTable(nameof(GlutRunAttribute)).HasKey(x => new { x.GlutProjectName, x.GlutProjectRunId, x.AttributeName });
        }
    }
}
