using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DFD.Core.Models;

public class Folder
{
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      public required string Name { get; set; }

      public string? Describtion { get; set; }

      public DateOnly CreatedAt { get; set; }

      public DateOnly UpdatedAt { get; set; }

      //navs
      public IList<FileRecord> Files { get; set; } = [];
}
