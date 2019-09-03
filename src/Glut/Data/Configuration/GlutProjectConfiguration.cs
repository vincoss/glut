using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Glut.Data.Configuration
{
    public class GlutProjectConfiguration : IEntityTypeConfiguration<GlutProject>
    {
        public void Configure(EntityTypeBuilder<GlutProject> builder)
        {
            builder.ToTable(nameof(GlutProject))
                   .HasIndex(x => new { x.GlutProjectName, x.ModifiedDateTimeUtc });

            builder.HasKey(x => x.GlutProjectName);
            builder.Property(t => t.GlutProjectName)
                   .IsRequired()
                   .HasColumnType("TEXT COLLATE NOCASE");
        }
    }
}
