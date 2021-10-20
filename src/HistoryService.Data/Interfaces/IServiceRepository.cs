using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceRepository
  {
    Task<Guid?> CreateAsync(DbService dbService);

    Task<bool> DoesNameExistAsync(string name);

    Task<List<DbService>> FindAsync();

    Task<DbService> GetAsync(Guid serviceId);

    Task<bool> EditAsync(Guid serviceId, JsonPatchDocument<DbService> request);
  }
}