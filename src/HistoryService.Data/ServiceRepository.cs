using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Data
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IDataProvider _provider;

        public ServiceRepository(
            IDataProvider provider)
        {
            _provider = provider;
        }

        public bool IsServiceNameExist(string name)
        {
            return _provider.Services.Any(p => p.Name.Contains(name));
        }

        public Guid Create(DbService dbService)
        {
            if (dbService == null)
            {
                throw new ArgumentNullException(nameof(dbService));
            }

            _provider.Services.Add(dbService);
            _provider.Save();

            return dbService.Id;
        }

        public List<DbService> Find()
        {
            return _provider.Services.ToList();
        }
    }
}
