using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Data.Interfaces
{
  [AutoInject]
  public interface IServiceRepository
  {
    Guid? Create(DbService dbService);

    bool DoesNameExist(string name);

    List<DbService> Find();

    DbService Get(Guid serviceId);

    bool Edit(DbService service, JsonPatchDocument<DbService> request);
  }
}