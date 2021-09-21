using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Data
{
  public class ServiceHistoryRepository : IServiceHistoryRepository
    {
        private readonly IDataProvider _provider;

        public ServiceHistoryRepository(
            IDataProvider provider)
        {
            _provider = provider;
        }

        public bool DoesServiceHistoryVersionExist(string version, Guid id)
        {
           return _provider.ServicesHistories.Any(sh => id == sh.ServiceId && sh.Version.Contains(version));
        }

        public Guid Create(DbServiceHistory dbServiceHistory)
        {
            if (dbServiceHistory == null)
            {
                return Guid.Empty;
            }

            _provider.ServicesHistories.Add(dbServiceHistory);
            _provider.Save();

            return dbServiceHistory.Id;
        }

        public IEnumerable<DbServiceHistory> Find(FindServicesHistoriesFilter filter, out int totalCount)
        {
            var dbServicesHistories = _provider.ServicesHistories
              .AsQueryable();

            if (filter.ServiceId.HasValue)
            {
                dbServicesHistories = dbServicesHistories.Where(sh => sh.ServiceId == filter.ServiceId.Value);
            }
            if (!string.IsNullOrEmpty(filter.Verison))
            {
                dbServicesHistories = dbServicesHistories.Where(sh => sh.Version == filter.Verison);
            }

            totalCount = dbServicesHistories.Count();

            return dbServicesHistories.Skip(filter.skipCount).Take(filter.takeCount).ToList();
        }
    }
}
