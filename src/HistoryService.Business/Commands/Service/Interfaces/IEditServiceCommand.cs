using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces
{
  [AutoInject]
  public interface IEditServiceCommand
  {
    Task<OperationResultResponse<bool>> ExecuteAsync(Guid serviceId, JsonPatchDocument<EditServiceRequest> request);
  }
}
