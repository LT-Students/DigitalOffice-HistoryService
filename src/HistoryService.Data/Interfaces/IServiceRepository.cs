using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Data.Interfaces
{
    [AutoInject]
    public interface IServiceRepository
    {
        Guid Create(DbService dbService);

        bool DoesServiceNameExist(string name);

        List<DbService> Find();
    }
}