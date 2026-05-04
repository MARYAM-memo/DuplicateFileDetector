using DFD.Application.ViewModels.UploadAttempts;
using DFD.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DFD.MVC.Controllers
{
    public class UploadAttemptsController(IUnitOfWork uOW) : Controller
    {
        readonly IUnitOfWork unitOfWork = uOW;

        public async Task<ActionResult> Index()
        {
            var uploads = await unitOfWork.Uploads.FetchAsync(true);
            var vm = uploads.Select(u => new UploadAttemptsListVM
            {
                Id = u.Id,
                FileName = u.FileName,
                FileHash = u.FileHash,
                FileSize = u.FileSize,
                AttemptedAt = u.AttemptedAt,
                IsRejected = u.IsRejected,

            }).ToList();
            return View(vm);
        }

        //clean up old attempts
        public async Task<IActionResult> ClearAll()
        {
            await unitOfWork.Uploads.ExecuteDeleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
