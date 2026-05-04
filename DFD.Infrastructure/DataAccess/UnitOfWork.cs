using DFD.Core.Interfaces;
using DFD.Core.Models;
using DFD.Infrastructure.Data;

namespace DFD.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
      readonly DatabaseContext context;
      public UnitOfWork(DatabaseContext ctx)
      {
            context = ctx;
            Files = new DashboardRepository<FileRecord>(context);
            Folders = new Repository<Folder>(context);
            Uploads = new DashboardRepository<UploadAttempt>(context);
      }
      
      public IDashboardRepository<FileRecord> Files { get; }

      public IRepository<Folder> Folders { get; }

      public IDashboardRepository<UploadAttempt> Uploads { get; }

      public void Dispose()
      {
            context.Dispose();
            GC.SuppressFinalize(this);
      }

      public async Task<int> SaveChangesAsync()
      {
            return await context.SaveChangesAsync();
      }
}
