using Shared;

namespace DFD.Application.ViewModels.Files;


public class FileModel
{
      public int Id { get; set; }
      public string OriginalFileName { get; set; } = string.Empty;
      public string StoredFileName { get; set; } = string.Empty;
      public string FilePath { get; set; } = string.Empty;
      public long? FileSize { get; set; }
      public string ContentType { get; set; } = string.Empty;
      public string FileHash { get; set; } = string.Empty;
      public DateOnly UploadedAt { get; set; }
      public DateOnly? LastAccessedAt { get; set; }

      public string FormattedSize => Functions.FormatSize(FileSize ?? 0);
      public string ShortHash => Functions.ShortingString(FileHash, 16);
      public string FileIconClass => Functions.GetFileIconClass(ContentType);
      public string FileIcon => Functions.GetFileIcon(ContentType);
}