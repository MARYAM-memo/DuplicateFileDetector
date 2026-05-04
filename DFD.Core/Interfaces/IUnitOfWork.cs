using DFD.Core.Models;

namespace DFD.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
      IDashboardRepository<FileRecord> Files { get; }
      IRepository<Folder> Folders { get; }
      IDashboardRepository<UploadAttempt> Uploads { get; }

      Task<int> SaveChangesAsync();
}
