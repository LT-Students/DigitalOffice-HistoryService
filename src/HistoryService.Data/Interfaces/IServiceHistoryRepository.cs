using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;

namespace LT.DigitalOffice.HistoryService.Data.Interfaces
{
    [AutoInject]
    public interface IServiceHistoryRepository
    {
        Guid Create(DbServiceHistory dbServiceHistory);
    }
}