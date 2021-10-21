using FluentValidation;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.HistoryService.Validation.Service.Interfaces
{
  [AutoInject]
  public interface IEditServiceValidator : IValidator<JsonPatchDocument<EditServiceRequest>>
  {
  }
}
