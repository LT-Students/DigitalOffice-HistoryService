using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Data
{
  public class ServiceHistoryRepository : IServiceHistoryRepository
  {
    private readonly IDataProvider _provider;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ServiceHistoryRepository(
      IDataProvider provider,
      IHttpContextAccessor httpContextAccessor)
    {
      _provider = provider;
      _httpContextAccessor = httpContextAccessor;
    }

    public bool DoesVersionExist(string version, Guid id)
    {
      return _provider.ServicesHistories.Any(sh => id == sh.ServiceId && sh.Version.Contains(version));
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

    public List<DbServiceHistory> Find(FindServicesHistoriesFilter filter, out int totalCount)
    {
      IQueryable<DbServiceHistory> dbServicesHistories = _provider.ServicesHistories.AsQueryable();

      if (filter.ServiceId.HasValue)
      {
        dbServicesHistories = dbServicesHistories.Where(sh => sh.ServiceId == filter.ServiceId.Value);
      }
      if (!string.IsNullOrEmpty(filter.Verison))
      {
        dbServicesHistories = dbServicesHistories.Where(sh => sh.Version == filter.Verison);
      }

      totalCount = dbServicesHistories.Count();

      return dbServicesHistories.Skip(filter.SkipCount).Take(filter.TakeCount).OrderByDescending(v => v.Version).ToList();
    }

    public DbServiceHistory Get(Guid serviceHistoryId)
    {
      DbServiceHistory serviceHistory = _provider.ServicesHistories.FirstOrDefault(e => e.Id == serviceHistoryId);
      if (serviceHistory == null)
      {
        return null;
      }

      return serviceHistory;
    }

    public bool Edit(DbServiceHistory serviceHistory, JsonPatchDocument<DbServiceHistory> request)
    {
      if (serviceHistory == null)
      {
        return false;
      }

      if (request == null)
      {
        return false;
      }

      request.ApplyTo(serviceHistory);
      serviceHistory.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      serviceHistory.ModifiedAtUtc = DateTime.UtcNow;
      _provider.Save();

      return true;
    }
  }
}
