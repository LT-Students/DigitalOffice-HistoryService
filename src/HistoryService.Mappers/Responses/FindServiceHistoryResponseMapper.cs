using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using System;

namespace LT.DigitalOffice.HistoryService.Mappers.Responses
{
    public class FindServiceHistoryResponseMapper : IFindServiceHistoryResponseMapper
    {
        public ServiceHistoryInfo Map(DbServiceHistory dbServiceHistory)
        {
            if (dbServiceHistory == null)
            {
                throw new ArgumentNullException(nameof(dbServiceHistory));
            }

            return new ServiceHistoryInfo
            {
                Id = dbServiceHistory.Id,
                ServiceId = dbServiceHistory.ServiceId,
                Version = dbServiceHistory.Version,
                Content = dbServiceHistory.Content,
                CreatedBy = dbServiceHistory.CreatedBy,
                CreatedAtUtc = dbServiceHistory.CreatedAtUtc
            };
        }
    }
}
