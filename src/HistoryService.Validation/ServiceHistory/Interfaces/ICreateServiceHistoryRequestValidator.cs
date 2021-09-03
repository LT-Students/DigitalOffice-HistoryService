using FluentValidation;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.HistoryService.Validation.ServiceHistory.Interfaces
{
    [AutoInject]
    public interface ICreateServiceHistoryRequestValidator : IValidator<CreateServiceHistoryRequest>
    {
    }
}
