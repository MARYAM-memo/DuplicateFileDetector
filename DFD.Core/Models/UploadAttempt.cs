using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DFD.Core.Models;

public class UploadAttempt
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public required string FileName { get; set; }
      public string? FileHash { get; set; }
      public long? FileSize { get; set; }
      public DateOnly AttemptedAt { get; set; }
      public bool IsRejected { get; set; }
}
