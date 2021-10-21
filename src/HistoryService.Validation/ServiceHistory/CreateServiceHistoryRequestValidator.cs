using FluentValidation;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.ServiceHistory.Interfaces;

namespace LT.DigitalOffice.HistoryService.Validation.ServiceHistory
{
  public class CreateServiceHistoryRequestValidator : AbstractValidator<CreateServiceHistoryRequest>, ICreateServiceHistoryRequestValidator
  {
    public CreateServiceHistoryRequestValidator(IServiceHistoryRepository repository)
    {
      RuleFor(sh => sh.Version)
        .Cascade(CascadeMode.Stop).NotNull().NotEmpty()
        .WithMessage("Version cannot be empty.")
        .MaximumLength(15)
        .WithMessage("Version is too long.");

      RuleFor(sh => sh.Content)
        .NotEmpty()
        .WithMessage("Content cannot be empty.");

      RuleFor(sh => sh)
        .MustAsync(async (sh, _) => !await repository.DoesVersionExistAsync(sh.Version, sh.ServiceId))
        .WithMessage("History version for this service already exist");
    }
  }
}
