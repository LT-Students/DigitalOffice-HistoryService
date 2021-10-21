using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces
{
  [AutoInject]
    public interface IFindServiceCommand
    {
      Task<FindResultResponse<ServiceInfo>> ExecuteAsync();
    }
}