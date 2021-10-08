using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces
{
  [AutoInject]
  public interface ICreateServiceCommand
  {
    OperationResultResponse<Guid?> Execute(CreateServiceRequest request);
  }
}