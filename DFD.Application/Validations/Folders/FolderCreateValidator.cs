using DFD.Application.ViewModels.Folders;
using FluentValidation;

namespace DFD.Application.Validations.Folders;

public class FolderCreateValidator : AbstractValidator<FolderCreateVM>
{
      public FolderCreateValidator()
      {
            RuleFor(r => r.Name).NotEmpty().WithMessage("اسم المجلد مطلوب").MaximumLength(100).WithMessage("اسم المجلد لا يمكن أن يتجاوز 100 حرف");
            RuleFor(r => r.Description).MaximumLength(500).WithMessage("وصف المجلد لا يمكن أن يتجاوز 500 حرف");
      }
}
