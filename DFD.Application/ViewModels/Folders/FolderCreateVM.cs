using System.ComponentModel.DataAnnotations;

namespace DFD.Application.ViewModels.Folders;

public class FolderCreateVM
{
      [Display(Name= "اسم المجلد")]
      public required string Name { get; set; }

      [Display(Name = "الوصف")]
      public string? Description { get; set; }
}
