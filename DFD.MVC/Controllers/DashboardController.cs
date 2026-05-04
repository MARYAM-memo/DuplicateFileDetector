using DFD.Application.ViewModels;
using DFD.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace DFD.MVC.Controllers
{
    public class DashboardController(IUnitOfWork uOW) : Controller
    {
        readonly IUnitOfWork unitOfWork = uOW;
        public async Task<ActionResult> Index()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var fiveDaysAgo = today.AddDays(-5);

            var recentFiles = await unitOfWork.Files.FetchAsync(withNoTracking: true, predicate: f => f.UploadedAt >= fiveDaysAgo && f.UploadedAt <= today);
            var recentAttempts = await unitOfWork.Uploads.FetchAsync(withNoTracking: true, predicate: u => u.AttemptedAt >= fiveDaysAgo && u.AttemptedAt <= today);

            var filesStats = await unitOfWork.Files.GetFileStatsAsync(g => new FileStatsDto
            {
                TotalFiles = g.Count(),
                TotalStorageUsed = g.Sum(x => x.FileSize ?? 0),
                ImagesCount = g.Count(x => x.ContentType.StartsWith("image/")),
                PdfsCount = g.Count(x => x.ContentType == "application/pdf"),
                OtherFilesCount = g.Count(x =>
                    x.ContentType != "application/pdf" &&
                    !x.ContentType.StartsWith("image/")),
                RecentFiles = g.OrderByDescending(f => f.UploadedAt)
                    .Take(10)
                    .Select(f => new RecentFileViewModel
                    {
                        Id = f.Id,
                        OriginalFileName = f.OriginalFileName,
                        FolderName = f.Folder != null ? f.Folder.Name : null,
                        UploadedAt = f.UploadedAt,
                        FileIcon = Functions.GetFileIcon(f.ContentType),
                    }).ToList(),
            });

            var uploadsStats = await unitOfWork.Uploads.GetFileStatsAsync(g => new UploadAttemptsStatsDto
            {
                TotalUploadAttempts = g.Count(),
                RejectedAttempts = g.Count(u => u.IsRejected),
                RecentAttempts = g.OrderByDescending(u => u.AttemptedAt)
                    .Take(10)
                    .Select(u => new RecentAttemptViewModel
                    {
                        Id = u.Id,
                        FileName = u.FileName,
                        AttemptedAt = u.AttemptedAt,
                        IsRejected = u.IsRejected,
                    }).ToList(),
            });

            var dashboardStats = new DashboardStatsVM
            {
                TotalFolders = await unitOfWork.Folders.CountAsync(),
                FilesStats = filesStats ?? new FileStatsDto(),
                UploadsStats = uploadsStats ?? new UploadAttemptsStatsDto(),
            };

            return View(dashboardStats);
        }
    }
}
