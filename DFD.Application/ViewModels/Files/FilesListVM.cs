namespace DFD.Application.ViewModels.Files;

public class FilesListVM
{
      public int Id { get; set; }

      public required string OriginalFileName { get; set; }
      public required string StoredFileName { get; set; }
      public required string FilePath { get; set; }
      public long? FileSize { get; set; }
      public required string ContentType { get; set; }
      public required string FileHash { get; set; }
      public DateOnly UploadedAt { get; set; }
      public DateOnly? LastAccessedAt { get; set; }
      public required string FolderName { get; set; }
      public required string DownloadUrl { get; set; }
}
