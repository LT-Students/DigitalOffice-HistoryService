using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbServiceMapper
  {
    DbService Map(CreateServiceRequest request);
  }
}
