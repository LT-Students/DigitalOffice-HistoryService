using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using System;

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

        public Guid Create(DbServiceHistory dbServiceHistory)
        {
            if (dbServiceHistory == null)
            {
                throw new ArgumentNullException(nameof(dbServiceHistory));
            }

            _provider.ServicesHistories.Add(dbServiceHistory);
            _provider.Save();

            return dbServiceHistory.Id;
        }
    }
}
