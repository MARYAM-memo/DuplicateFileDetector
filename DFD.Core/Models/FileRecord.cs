using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DFD.Core.Models;

public class FileRecord
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public required string OriginalFileName { get; set; }
      public required string StoredFileName { get; set; }
      public required string FilePath { get; set; }
      public long? FileSize { get; set; }
      public required string ContentType { get; set; }
      public required string FileHash { get; set; }
      public DateOnly UploadedAt { get; set; }
      public DateOnly? LastAccessedAt { get; set; }

      //fk
      public int FolderId { get; set; }
      public Folder? Folder { get; set; }
}
