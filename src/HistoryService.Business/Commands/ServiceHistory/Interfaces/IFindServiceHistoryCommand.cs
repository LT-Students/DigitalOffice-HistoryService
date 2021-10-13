using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces
{
  [AutoInject]
    public interface IFindServiceHistoryCommand
    {
      Task<FindResultResponse<ServiceHistoryInfo>> ExecuteAsync(FindServicesHistoriesFilter filter);
    }
}
