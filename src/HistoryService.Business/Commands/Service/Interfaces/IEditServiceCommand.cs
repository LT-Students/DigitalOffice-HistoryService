using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces
{
  [AutoInject]
  public interface IEditServiceCommand
  {
    OperationResultResponse<bool> Execute(Guid serviceId, JsonPatchDocument<EditServiceRequest> request);
  }
}
