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
                   .HasIndex(x => new { x.GlutRunAttributeId, x.GlutProjectName, x.GlutProjectRunId, x.AttributeName }).IsUnique();

            builder.HasKey(x => x.GlutRunAttributeId);

            builder.Property(t => t.GlutRunAttributeId)
                   .IsRequired()
                   .HasColumnType("INTEGER");

            builder.Property(t => t.GlutProjectName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(64) COLLATE NOCASE");

            builder.Property(t => t.GlutProjectRunId)
                   .IsRequired()
                   .HasColumnType("INT");

            builder.Property(t => t.AttributeName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(64) COLLATE NOCASE");

            builder.Property(t => t.AttributeValue)
                   .IsRequired()
                   .HasColumnType("VARCHAR(64) COLLATE NOCASE");

            builder.Property(x => x.CreatedDateTimeUtc)
                  .IsRequired()
                  .HasColumnType("DATETIME");

            builder.Property(x => x.CreatedByUserName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(64) COLLATE NOCASE");
        }
    }
}
