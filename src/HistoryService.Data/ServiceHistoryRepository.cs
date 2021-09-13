using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Exceptions.Models;
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

        public bool IsServiceHistoryVersionExist(string version)
        {
            return _provider.ServicesHistories.Any(p => p.Version.Contains(version));
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

        public IEnumerable<DbServiceHistory> Find(FindServicesHistoriesFilter filter, int skipCount, int takeCount, out int totalCount)
        {
            if (skipCount < 0)
            {
                throw new BadRequestException("Skip count can't be less than 0.");
            }

            if (takeCount < 1)
            {
                throw new BadRequestException("Take count can't be equal or less than 0.");
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

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

            return dbServicesHistories.Skip(skipCount).Take(takeCount).ToList();
        }
    }
}
