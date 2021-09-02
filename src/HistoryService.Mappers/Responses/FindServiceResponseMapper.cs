using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using System;

namespace LT.DigitalOffice.HistoryService.Mappers.Responses
{
    public class FindServiceResponseMapper : IFindServiceResponseMapper
    {
        public ServiceInfo Map(DbService dbService)
        {
            if (dbService == null)
            {
                throw new ArgumentNullException(nameof(dbService));
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
