using DFD.Core.Models;
using DFD.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DFD.Infrastructure.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
      public DbSet<Folder> Folders { get; set; }
      public DbSet<FileRecord> Files { get; set; }
      public DbSet<UploadAttempt> Uploads { get; set; }
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
            base.OnConfiguring(optionsBuilder);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FoldersConfiguration());
            modelBuilder.ApplyConfiguration(new FilesConfiguration());
            modelBuilder.ApplyConfiguration(new UploadAttemptsConfiguration());
      }
}
