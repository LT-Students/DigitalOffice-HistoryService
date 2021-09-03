using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces
{
    [AutoInject]
    public interface ICreateServiceHistoryCommand
    {
        OperationResultResponse<Guid> Execute(CreateServiceHistoryRequest request);
    }
}
