using System;
using Shared;

namespace DFD.Application.ViewModels;

public class DashboardStatsVM
{
      public int TotalFolders { get; set; }
      public required FileStatsDto FilesStats { get; set; }
      public required UploadAttemptsStatsDto UploadsStats { get; set; }

}

public class RecentFileViewModel
{
      public int Id { get; set; }
      public string OriginalFileName { get; set; } = string.Empty;
      public string? FolderName { get; set; }
      public DateOnly UploadedAt { get; set; }
      public string FileIcon { get; set; } = "fa-file";
}
public class RecentAttemptViewModel
{
      public int Id { get; set; }
      public string FileName { get; set; } = string.Empty;
      public DateOnly AttemptedAt { get; set; }
      public bool IsRejected { get; set; }
      public string StatusIcon => IsRejected ? "fa-ban" : "fa-check";
}

public class UploadAttemptsStatsDto
{
      public int TotalUploadAttempts { get; set; }
      public int RejectedAttempts { get; set; }
      public List<RecentAttemptViewModel> RecentAttempts { get; set; } = [];

}
public class FileStatsDto
{
      public int TotalFiles { get; set; }

      public long TotalStorageUsed { get; set; }
      public long TotalStorageAvailable { get; set; } = 10L * 1024 * 1024 * 1024; // 10 GB default
      public string FormattedTotalStorage => Functions.FormatSize(TotalStorageUsed);
      public string FormattedAvailableStorage => Functions.FormatSize(TotalStorageAvailable);
      public double StoragePercentage => TotalStorageAvailable > 0
          ? (double)TotalStorageUsed / TotalStorageAvailable * 100
          : 0;
      public string StorageColor =>
StoragePercentage > 90 ? "var(--danger)" :
StoragePercentage > 70 ? "var(--warning)" :
"linear-gradient(90deg, var(--primary), var(--primary-light))";
      public int ImagesCount { get; set; }
      public int PdfsCount { get; set; }
      public int OtherFilesCount { get; set; }
      public List<RecentFileViewModel> RecentFiles { get; set; } = [];

}