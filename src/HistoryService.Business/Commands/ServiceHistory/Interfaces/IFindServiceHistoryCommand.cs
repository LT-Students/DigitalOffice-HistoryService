using LT.DigitalOffice.HistoryService.Models.Dto.Models;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces
{
    [AutoInject]
    public interface IFindServiceHistoryCommand
    {
        FindResponse<ServiceHistoryInfo> Execute(FindServicesHistoriesFilter filter);
    }
}
