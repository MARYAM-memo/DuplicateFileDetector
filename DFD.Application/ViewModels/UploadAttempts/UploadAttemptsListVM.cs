using System.ComponentModel.DataAnnotations;
using Shared;

namespace DFD.Application.ViewModels.UploadAttempts;

public class UploadAttemptsListVM
{
      public int Id { get; set; }

      [Display(Name = "اسم الملف")]
      public string FileName { get; set; } = string.Empty;

      [Display(Name = "الـ Hash")]
      public string? FileHash { get; set; }

      [Display(Name = "الحجم")]
      public long? FileSize { get; set; }

      [Display(Name = "تاريخ المحاولة")]
      [DataType(DataType.Date)]
      public DateOnly AttemptedAt { get; set; }

      [Display(Name = "الحالة")]
      public bool IsRejected { get; set; }

      // Helper properties
      public string FormattedSize => Functions.FormatSize(FileSize ?? 0);
      public string ShortHash => Functions.ShortingString(FileHash??"-", 20);

      public string StatusBadgeClass => IsRejected ? "badge-danger" : "badge-success";
      public string StatusText => IsRejected ? "مرفوض - ملف مكرر" : "ناجح";
      public string StatusIcon => IsRejected ? "fa-ban" : "fa-check";
}
