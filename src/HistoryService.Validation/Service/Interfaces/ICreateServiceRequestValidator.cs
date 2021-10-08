using FluentValidation;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.HistoryService.Validation.Service.Interfaces
{
  [AutoInject]
  public interface ICreateServiceRequestValidator : IValidator<CreateServiceRequest>
  {
  }
}
