using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces
{
    [AutoInject]
    public interface IFindServiceCommand
    {
        OperationResultResponse<List<FindResultResponse>> Execute();
    }
}