using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces
{
  [AutoInject]
    public interface IFindServiceHistoryCommand
    {
      FindResultResponse<ServiceHistoryInfo> Execute(FindServicesHistoriesFilter filter);
    }
}
