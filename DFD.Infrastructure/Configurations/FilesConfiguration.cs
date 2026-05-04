using DFD.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFD.Infrastructure.Configurations;

public class FilesConfiguration : IEntityTypeConfiguration<FileRecord>
{
      public void Configure(EntityTypeBuilder<FileRecord> builder)
      {
            //key
            builder.HasKey(p => p.Id);

            //properties
            builder.Property(p => p.OriginalFileName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.StoredFileName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.FilePath).IsRequired().HasMaxLength(500);
            builder.Property(p => p.ContentType).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FileHash).IsRequired().HasMaxLength(64).IsFixedLength();
            builder.Property(p => p.UploadedAt).HasColumnType("date");
            builder.Property(p => p.LastAccessedAt).HasColumnType("date");
           
            //navigation forign key
            builder.HasOne(p => p.Folder).WithMany(p => p.Files).HasForeignKey(p => p.FolderId).OnDelete(DeleteBehavior.Restrict);

            //indexes
            builder.HasIndex(p => p.FileHash).IsUnique();
            builder.HasIndex(e => new { e.FolderId, e.FileHash }).IsUnique();
            builder.HasIndex(p => p.UploadedAt);
      }
}
