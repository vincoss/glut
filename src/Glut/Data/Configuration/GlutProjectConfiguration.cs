using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Glut.Data.Configuration
{
    public class GlutProjectConfiguration : IEntityTypeConfiguration<GlutProject>
    {
        public void Configure(EntityTypeBuilder<GlutProject> builder)
        {
            builder.ToTable(nameof(GlutProject))
                   .HasIndex(x => new { x.GlutProjectName, x.ModifiedDateTimeUtc }).IsUnique();

            builder.HasKey(x => x.GlutProjectName);
            builder.Property(t => t.GlutProjectName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(64) COLLATE NOCASE");

            builder.Property(x => x.CreatedDateTimeUtc)
                   .IsRequired()
                   .HasColumnType("DATETIME");

            builder.Property(x => x.ModifiedDateTimeUtc)
                   .IsRequired()
                   .HasColumnType("DATETIME");

            builder.Property(x => x.CreatedByUserName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(64) COLLATE NOCASE");
        }
    }
}
