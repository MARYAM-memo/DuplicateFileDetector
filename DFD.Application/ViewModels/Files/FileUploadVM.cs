using System;
using Microsoft.AspNetCore.Http;

namespace DFD.Application.ViewModels.Files;

public class FileUploadVM
{
      public IFormFile? File { get; set; }
      public int FolderId { get; set; }

      //بعد الرفع
      public string? CalculatedHash { get; set; }
      public bool IsDuplicate { get; set; }
      public string? DuplicateMessage { get; set; }

      //for select
      public List<FolderOptionViewModel> AvailableFolders { get; set; } = [];
}
public class FolderOptionViewModel
{
      public int Id { get; set; }
      public string Name { get; set; } = string.Empty;
}