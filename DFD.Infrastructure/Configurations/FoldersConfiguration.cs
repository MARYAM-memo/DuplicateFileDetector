using DFD.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFD.Infrastructure.Configurations;

public class FoldersConfiguration : IEntityTypeConfiguration<Folder>
{
      public void Configure(EntityTypeBuilder<Folder> builder)
      {
            //key
            builder.HasKey(p => p.Id);

            //properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Describtion).HasMaxLength(500);
            builder.Property(p => p.CreatedAt).HasColumnType("date");
            builder.Property(p => p.UpdatedAt).HasColumnType("date");

            //indexes
            builder.HasIndex(e => e.Name)
                .IsUnique();
      }
}
