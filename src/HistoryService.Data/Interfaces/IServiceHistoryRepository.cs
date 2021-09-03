using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Data.Interfaces
{
    [AutoInject]
    public interface IServiceHistoryRepository
    {
        Guid Create(DbServiceHistory dbServiceHistory);

        bool IsServiceHistoryVersionExist(string version);

        IEnumerable<DbServiceHistory> Find(FindServicesHistoriesFilter filter, int skipCount, int takeCount, out int totalCount);
    }
}