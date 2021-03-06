using FluentValidation;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;

namespace LT.DigitalOffice.HistoryService.Validation.Service
{
  public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>, ICreateServiceRequestValidator
  {
    public CreateServiceRequestValidator(IServiceRepository repository)
    {
      RuleFor(service => service.Name)
        .Cascade(CascadeMode.Stop).NotNull().NotEmpty()
        .WithMessage("Name cannot be empty.")
        .MaximumLength(30)
        .WithMessage("Name is too long.")
        .MustAsync(async (name, _) => !await repository.DoesNameExistAsync(name))
        .WithMessage("Service with name already exist");
    }
  }
}
