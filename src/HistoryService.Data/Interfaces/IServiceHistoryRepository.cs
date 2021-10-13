using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceHistoryRepository
  {
    Task<Guid> CreateAsync(DbServiceHistory dbServiceHistory);

    Task<bool> DoesVersionExistAsync(string version, Guid id);

    Task<(List<DbServiceHistory> dbServicesHistories, int totalCount)> FindAsync(FindServicesHistoriesFilter filter);

    Task<DbServiceHistory> GetAsync(Guid serviceHistoryId);

    Task<bool> EditAsync(DbServiceHistory service, JsonPatchDocument<DbServiceHistory> request);

  }
}