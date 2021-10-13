using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces
{
  [AutoInject]
  public interface ICreateServiceHistoryCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateServiceHistoryRequest request);
  }
}
