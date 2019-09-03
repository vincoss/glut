using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Glut.Data.Configuration
{
    public class GlutResultItemConfiguration : IEntityTypeConfiguration<GlutResultItem>
    {
        public void Configure(EntityTypeBuilder<GlutResultItem> builder)
        {
            builder.ToTable(nameof(GlutResultItem))
                   .HasIndex(x => new { x.GlutProjectName, x.GlutProjectRunId, x.StatusCode });

            builder.HasKey(x => x.GlutResultId);

            builder.Property(t => t.GlutProjectName).IsRequired()
                   .HasColumnType("TEXT COLLATE NOCASE");
        }
    }
}
