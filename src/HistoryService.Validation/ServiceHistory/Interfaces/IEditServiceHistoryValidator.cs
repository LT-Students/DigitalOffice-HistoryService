using FluentValidation;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.HistoryService.Validation.ServiceHistory.Interfaces
{
  [AutoInject]
  public interface IEditServiceHistoryValidator : IValidator<JsonPatchDocument<EditServiceHistoryRequest>>
  {
  }
}
