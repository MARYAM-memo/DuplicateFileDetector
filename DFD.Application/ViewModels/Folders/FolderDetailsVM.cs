using DFD.Application.ViewModels.Files;

namespace DFD.Application.ViewModels.Folders;

public class FolderDetailsVM
{
      public int Id { get; set; }
      public string Name { get; set; } = string.Empty;
      public string? Description { get; set; }
      public DateOnly CreatedAt { get; set; }
      public DateOnly UpdatedAt { get; set; }

      public List<FileModel> Files { get; set; } = [];
}
