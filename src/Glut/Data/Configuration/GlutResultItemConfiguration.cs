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
                   .HasIndex(x => new
                   {
                       x.GlutResultId,
                       x.GlutProjectName, x.GlutProjectRunId,
                       x.StartDateTimeUtc, x.EndDateTimeUtc,
                       x.RequestUri, x.IsSuccessStatusCode, x.StatusCode
                   });

            builder.HasKey(x => x.GlutResultId);

            builder.Property(t => t.GlutResultId)
                   .IsRequired()
                   .HasColumnType("INTEGER");

            builder.Property(t => t.GlutProjectName)
                   .IsRequired()
                   .HasColumnType("TEXT COLLATE NOCASE");

            builder.Property(t => t.GlutProjectRunId)
                   .IsRequired()
                   .HasColumnType("INT");

            builder.Property(x => x.StartDateTimeUtc)
                   .IsRequired()
                   .HasColumnType("DATETIME");

            builder.Property(x => x.EndDateTimeUtc)
                   .IsRequired()
                   .HasColumnType("DATETIME");

            builder.Property(t => t.RequestUri)
                   .IsRequired()
                   .HasColumnType("VARCHAR(1024) COLLATE NOCASE");

            builder.Property(x => x.IsSuccessStatusCode)
                  .IsRequired()
                  .HasColumnType("BOOLEAN");

            builder.Property(t => t.StatusCode)
                  .IsRequired()
                  .HasColumnType("INT");

            builder.Property(t => t.HeaderLength)
                  .IsRequired()
                  .HasColumnType("BIGINT");

            builder.Property(t => t.ResponseLength)
                 .IsRequired()
                 .HasColumnType("BIGINT");

            builder.Property(t => t.TotalLegth)
               .IsRequired()
               .HasColumnType("BIGINT");

            builder.Property(t => t.RequestSentTicks)
                  .IsRequired()
                  .HasColumnType("BIGINT");

            builder.Property(t => t.ResponseTicks)
                 .IsRequired()
                 .HasColumnType("BIGINT");

            builder.Property(t => t.TotalTicks)
               .IsRequired()
               .HasColumnType("BIGINT");

            builder.Property(t => t.ResponseHeaders)
                  .IsRequired()
                  .HasColumnType("TEXT COLLATE NOCASE");

            builder.Property(t => t.Exception)
                 .HasColumnType("TEXT COLLATE NOCASE");

            builder.Property(x => x.CreatedDateTimeUtc)
                 .IsRequired()
                 .HasColumnType("DATETIME");

            builder.Property(x => x.CreatedByUserName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(64) COLLATE NOCASE");
        }
    }
}
