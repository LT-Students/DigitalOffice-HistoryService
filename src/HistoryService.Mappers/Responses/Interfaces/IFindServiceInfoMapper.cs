using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces
{
    [AutoInject]
    public interface IFindServiceInfoMapper
    {
        ServiceInfo Map(DbService dbService);
    }
}
