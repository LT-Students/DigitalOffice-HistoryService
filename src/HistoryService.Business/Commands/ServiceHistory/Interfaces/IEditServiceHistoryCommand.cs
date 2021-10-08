using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces
{
  [AutoInject]
  public interface IEditServiceHistoryCommand
  {
    OperationResultResponse<bool> Execute(Guid serviceId, JsonPatchDocument<EditServiceHistoryRequest> request);
  }
}
