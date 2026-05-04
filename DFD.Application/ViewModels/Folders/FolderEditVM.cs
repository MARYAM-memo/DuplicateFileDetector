namespace DFD.Application.ViewModels.Folders;

public class FolderEditVM
{
      public int Id { get; set; }
      public required string Name { get; set; }
      public string? Description { get; set; }
      public DateOnly UpdatedAt { get; set; }
}
