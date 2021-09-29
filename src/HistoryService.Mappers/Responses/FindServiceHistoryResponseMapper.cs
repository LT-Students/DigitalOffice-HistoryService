using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;

namespace LT.DigitalOffice.HistoryService.Mappers.Responses
{
  public class FindServiceHistoryResponseMapper : IFindServiceHistoryResponseMapper
  {
    public ServiceHistoryInfo Map(DbServiceHistory dbServiceHistory)
    {
      if (dbServiceHistory == null)
      {
        return null;
      }

      return new ServiceHistoryInfo
      {
        Id = dbServiceHistory.Id,
        Version = dbServiceHistory.Version,
        Content = dbServiceHistory.Content,
        CreatedBy = dbServiceHistory.CreatedBy,
        CreatedAtUtc = dbServiceHistory.CreatedAtUtc
      };
    }
  }
}
