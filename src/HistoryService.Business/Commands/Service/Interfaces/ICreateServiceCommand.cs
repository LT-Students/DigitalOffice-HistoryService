using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces
{
  [AutoInject]
  public interface ICreateServiceCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateServiceRequest request);
  }
}