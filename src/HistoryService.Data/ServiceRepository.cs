using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Data
{
  public class ServiceRepository : IServiceRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServiceRepository(
        IDataProvider provider,
        IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public bool DoesNameExist(string name)
    {
      return _provider.Services.Any(s => s.Name.Contains(name));
    }

    public Guid? Create(DbService dbService)
    {
      if (dbService == null)
      {
        return null;
      }

      _provider.Services.Add(dbService);
      _provider.Save();

      return dbService.Id;
    }

    public List<DbService> Find()
    {
      return _provider.Services.ToList();
    }

    public DbService Get(Guid serviceId)
    {
      DbService service = _provider.Services.FirstOrDefault(e => e.Id == serviceId);

      if (service == null)
      {
        return null;
      }

      return service;
    }

    public bool Edit(DbService service, JsonPatchDocument<DbService> request)
    {
      if (service == null)
      {
        return false;
      }

      if (request == null)
      {
        return false;
      }

      request.ApplyTo(service);
      service.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      service.ModifiedAtUtc = DateTime.UtcNow;
      _provider.Save();

      return true;
    }
  }
}
