using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces
{
  [AutoInject]
  public interface IEditServiceHistoryCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid serviceId, JsonPatchDocument<EditServiceHistoryRequest> request);
  }
}
