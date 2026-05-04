using DFD.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFD.Infrastructure.Configurations;

public class UploadAttemptsConfiguration : IEntityTypeConfiguration<UploadAttempt>
{
      public void Configure(EntityTypeBuilder<UploadAttempt> builder)
      {
            //key
            builder.HasKey(p => p.Id);
            
            //properties
            builder.Property(p => p.FileName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.AttemptedAt).HasColumnType("date");
            builder.Property(p => p.FileHash).HasMaxLength(64);
            
            //indexes
            builder.HasIndex(p => p.IsRejected);
            builder.HasIndex(p => p.AttemptedAt);
      }
}
