using DFD.Application.ViewModels.Files;
using DFD.Application.ViewModels.Folders;
using DFD.Core.Interfaces;
using DFD.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFD.MVC.Controllers
{
    public class FoldersController(IUnitOfWork uOW) : Controller
    {
        readonly IUnitOfWork unitOfWork = uOW;

        // GET: All Folders
        public async Task<ActionResult> Index()
        {
            var folders = await unitOfWork.Folders.FetchAsync(true, p => p.Files);

            var vms = folders.Select(f =>
            {
                var files = f.Files;
                long total = files.Sum(f => f.FileSize ?? 0);
                return new FoldersListVM
                {
                    Id = f.Id,
                    Name = f.Name,
                    Describtion = f.Describtion,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt,
                    FilesCount = files.Count(),
                    TotalFilesSize = total,
                };
            });
            return View(vms);
        }

        //create new folder
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FolderCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Folder folder = new()
                    {
                        Name = model.Name,
                        Describtion = model.Description,
                        CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    };

                    unitOfWork.Folders.Add(folder);
                    await unitOfWork.SaveChangesAsync();
                    TempData["SuccessMessage"] = "تم إنشاء المجلد بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the folder.\n\n:{ex}");
                    return View(model);
                }
            }
            TempData["ErrorMessage"] = "فشل إنشاء المجلد !!!";
            return View(model);
        }

        //Show Details of selected folder
        public async Task<IActionResult> Details(int id)
        {
            var folder = await unitOfWork.Folders.FindAsync(predicate: f => f.Id == id, args: f => f.Files);
            if (folder == null) return NotFoundFolder();
            var model = new FolderDetailsVM
            {
                Id = folder.Id,
                Name = folder.Name,
                Description = folder.Describtion,
                CreatedAt = folder.CreatedAt,
                UpdatedAt = folder.UpdatedAt,
                Files = [.. folder.Files.Select(s => new FileModel
                {
                    Id = s.Id,
                    OriginalFileName = s.OriginalFileName,
                    StoredFileName = s.StoredFileName,
                    FilePath = s.FilePath,
                    FileSize = s.FileSize,
                    ContentType = s.ContentType,
                    FileHash = s.FileHash,
                    UploadedAt = s.UploadedAt,
                    LastAccessedAt = s.LastAccessedAt,
                })],
            };
            return View(model);
        }

        //edit a selected folder
        public async Task<IActionResult> Edit(int id)
        {
            var folder = await unitOfWork.Folders.FindAsync(predicate: f => f.Id == id, args: f => f.Files);
            if (folder == null) return NotFoundFolder();
            var model = new FolderEditVM
            {
                Id = folder.Id,
                Name = folder.Name,
                Description = folder.Describtion,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FolderEditVM model, int id)
        {
            var folder = await unitOfWork.Folders.FindAsync(predicate: f => f.Id == id, args: f => f.Files);
            if (folder == null) return NotFoundFolder();
            if (ModelState.IsValid)
            {
                try
                {
                    folder.Name = model.Name;
                    folder.Describtion = model.Description;
                    folder.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
                    unitOfWork.Folders.Update(folder);
                    await unitOfWork.SaveChangesAsync();
                    TempData["SuccessMessage"] = "تم تعديل المجلد بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the folder.\n\n:{ex}");
                    return View(model);
                }
            }
            TempData["ErrorMessage"] = "فشل إنشاء المجلد !!!";
            return View(model);
        }

        //delete a selected folder
        public async Task<IActionResult> Delete(int id)
        {
            var folder = await unitOfWork.Folders.FindAsync(predicate: f => f.Id == id, args: f => f.Files);
            if (folder == null) return NotFoundFolder();

            var model = new FolderDetailsVM
            {
                Id = folder.Id,
                Name = folder.Name,
                Description = folder.Describtion,
                CreatedAt = folder.CreatedAt,
                UpdatedAt = folder.UpdatedAt,
                Files = [.. folder.Files.Select(s => new FileModel
                {
                    Id = s.Id,
                    OriginalFileName = s.OriginalFileName,
                    StoredFileName = s.StoredFileName,
                    FilePath = s.FilePath,
                    FileSize = s.FileSize,
                    ContentType = s.ContentType,
                    FileHash = s.FileHash,
                    UploadedAt = s.UploadedAt,
                    LastAccessedAt = s.LastAccessedAt,
                })],
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var folder = await unitOfWork.Folders.FindAsync(predicate: f => f.Id == id, args: f => f.Files);
            if (folder == null) return NotFoundFolder();

            unitOfWork.Folders.Delete(folder);
            await unitOfWork.SaveChangesAsync();

            TempData["SuccessMessage"] = "تم حذف المجلد بنجاح!";
            return RedirectToAction(nameof(Index));
        }


        private RedirectToActionResult NotFoundFolder()
        {
            TempData["ErrorMessage"] = "المجلد غير موجود!";
            return RedirectToAction(nameof(Index));
        }
    }
}
