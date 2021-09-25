using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using System;

namespace LT.DigitalOffice.HistoryService.Mappers.Responses
{
    public class FindResultResponseMapper : IFindResultResponseMapper
    {
        public ServiceInfo Map(DbService dbService)
        {
            if (dbService == null)
            {
                return null;
            }

            return new ServiceInfo
            {
                Id = dbService.Id,
                Name = dbService. Name,
                CreatedBy = dbService.CreatedBy,
                CreatedAtUtc = dbService.CreatedAtUtc
            };
        }
    }
}
