using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceHistoryRepository
  {
    Guid Create(DbServiceHistory dbServiceHistory);

    bool DoesVersionExist(string version, Guid id);

    List<DbServiceHistory> Find(FindServicesHistoriesFilter filter, out int totalCount);

    DbServiceHistory Get(Guid serviceHistoryId);

    bool Edit(DbServiceHistory service, JsonPatchDocument<DbServiceHistory> request);

  }
}