using System.ComponentModel.DataAnnotations;
using Shared;

namespace DFD.Application.ViewModels.Folders;

public class FoldersListVM
{
      public int Id { get; set; }

      [Display(Name = "اسم المجلد")]
      public required string Name { get; set; }

      [Display(Name = "الوصف")]
      public string? Describtion { get; set; }

      [Display(Name = "تاريخ الإنشاء")]
      [DataType(DataType.Date)]
      public DateOnly CreatedAt { get; set; }

      [Display(Name = "اَخر التحديث")]
      [DataType(DataType.Date)]
      public DateOnly UpdatedAt { get; set; }

      [Display(Name = "عدد الملفات")]
      public int FilesCount { get; set; }

      [Display(Name = "إجمالي حجم الملفات")]
      public long TotalFilesSize { get; set; }

      public string FormattedTotalSize => Functions.FormatSize(TotalFilesSize);
}
