using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Glut.Data.Configuration
{
    public class GlutRunAttributeConfiguration : IEntityTypeConfiguration<GlutRunAttribute>
    {
        public void Configure(EntityTypeBuilder<GlutRunAttribute> builder)
        {
            builder.ToTable(nameof(GlutRunAttribute))
                   .HasIndex(x => new { x.GlutProjectName, x.GlutProjectRunId, x.AttributeName });

            builder.HasKey(x => new { x.GlutProjectName, x.GlutProjectRunId, x.AttributeName });

            builder.Property(t => t.GlutProjectName).IsRequired()
                                                    .HasColumnType("TEXT COLLATE NOCASE");

            builder.Property(t => t.AttributeName).IsRequired()
                                                    .HasColumnType("TEXT COLLATE NOCASE");
        }
    }
}
