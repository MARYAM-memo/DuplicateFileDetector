using System.Security.Cryptography;
using DFD.Application.ViewModels.Files;
using DFD.Core.Interfaces;
using DFD.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFD.MVC.Controllers
{
    public class FilesController(IUnitOfWork uOW, IWebHostEnvironment host) : Controller
    {
        readonly IUnitOfWork unitOfWork = uOW;
        readonly IWebHostEnvironment env = host;
        // GET: All files
        public async Task<IActionResult> Index()
        {
            var files = await unitOfWork.Files.FetchAsync(true, op => op.Folder!);
            var vm = files.Select(f => new FilesListVM
            {
                Id = f.Id,
                OriginalFileName = f.OriginalFileName,
                StoredFileName = f.StoredFileName,
                FilePath = f.FilePath,
                FileSize = f.FileSize,
                ContentType = f.ContentType,
                FileHash = f.FileHash,
                UploadedAt = f.UploadedAt,
                LastAccessedAt = f.LastAccessedAt,
                FolderName = f.Folder?.Name ?? "",
                DownloadUrl = "_URL_VALID",
            });
            return View(vm);
        }

        //upload new file
        public async Task<IActionResult> Upload()
        {
            var folders = await unitOfWork.Folders.FetchAsync();
            var vm = new FileUploadVM
            {
                AvailableFolders = [.. folders.Select(f => new FolderOptionViewModel
                {
                    Id = f.Id,
                    Name = f.Name,
                })],
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(FileUploadVM model)
        {
            //if form not valid
            if (!ModelState.IsValid)
            {
                var folders = await unitOfWork.Folders.FetchAsync();
                model.AvailableFolders = [.. folders.Select(f => new FolderOptionViewModel { Id = f.Id, Name = f.Name })];
                return View(model);
            }

            //if there's no file
            var file = model.File;
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "الملف مطلوب");
                var folders = await unitOfWork.Folders.FetchAsync();
                model.AvailableFolders = [.. folders.Select(f => new FolderOptionViewModel { Id = f.Id, Name = f.Name })];
                return View(model);
            }

            //calc hashing
            string fileHash = "";
            using (var sha256 = SHA256.Create())
            using (var stream = file.OpenReadStream())
            {
                var hash = await sha256.ComputeHashAsync(stream);
                fileHash = Convert.ToHexStringLower(hash);
            }

            //check duplicate
            var existingFile = await unitOfWork.Files.FindAsync(predicate: f => f.FileHash == fileHash);
            if (existingFile != null)
            {
                var upload = new UploadAttempt
                {
                    FileName = file.FileName,
                    FileHash = fileHash,
                    FileSize = file.Length,
                    AttemptedAt = DateOnly.FromDateTime(DateTime.Now),
                    IsRejected = true,
                };
                unitOfWork.Uploads.Add(upload);
                await unitOfWork.SaveChangesAsync();
                ModelState.AddModelError("", "هذا الملف موجود بالفعل في النظام!");
                var folders = await unitOfWork.Folders.FetchAsync(true);
                model.AvailableFolders = [.. folders.Select(f => new FolderOptionViewModel { Id = f.Id, Name = f.Name })];
                model.IsDuplicate = true;
                return View(model);
            }

            //save file in directory
            var uploadsFolder = Path.Combine(env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var storedFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, storedFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //save to db
            var fileRecord = new FileRecord
            {
                OriginalFileName = file.FileName,
                StoredFileName = storedFileName,
                FilePath = $"/uploads/{storedFileName}",
                FileSize = file.Length,
                ContentType = file.ContentType,
                FileHash = fileHash,
                UploadedAt = DateOnly.FromDateTime(DateTime.Now),
                FolderId = model.FolderId,
            };
            unitOfWork.Files.Add(fileRecord);

            var uploadAttempt = new UploadAttempt
            {
                FileName = file.FileName,
                FileHash = fileHash,
                FileSize = file.Length,
                AttemptedAt = DateOnly.FromDateTime(DateTime.Now),
                IsRejected = false,
            };
            unitOfWork.Uploads.Add(uploadAttempt);

            await unitOfWork.SaveChangesAsync();
            TempData["SuccessMessage"] = $"تم رفع الملف '{file.FileName}' بنجاح!";
            return RedirectToAction(nameof(Index));
        }
    }
}
