using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public async Task<bool> DoesVersionExistAsync(string version, Guid id)
    {
      return await _provider.ServicesHistories.AnyAsync(sh => id == sh.ServiceId && sh.Version.Contains(version));
    }

    public async Task<Guid> CreateAsync(DbServiceHistory dbServiceHistory)
    {
      if (dbServiceHistory == null)
      {
        return Guid.Empty;
      }

      _provider.ServicesHistories.Add(dbServiceHistory);
      await _provider.SaveAsync();

      return dbServiceHistory.Id;
    }

    public async Task<(List<DbServiceHistory> dbServicesHistories, int totalCount)> FindAsync(FindServicesHistoriesFilter filter)
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

      return (await dbServicesHistories.Skip(filter.SkipCount).Take(filter.TakeCount).OrderByDescending(v => v.Version).ToListAsync(), await dbServicesHistories.CountAsync());
    }

    public async Task<DbServiceHistory> GetAsync(Guid serviceHistoryId)
    {
      DbServiceHistory serviceHistory = await _provider.ServicesHistories.FirstOrDefaultAsync(e => e.Id == serviceHistoryId);
      if (serviceHistory == null)
      {
        return null;
      }

      return serviceHistory;
    }

    public async Task<bool> EditAsync(DbServiceHistory serviceHistory, JsonPatchDocument<DbServiceHistory> request)
    {
      if (serviceHistory == null || request == null)
      {
        return false;
      }

      request.ApplyTo(serviceHistory);
      serviceHistory.ModifiedBy = _httpContextAccessor.HttpContext.GetUserId();
      serviceHistory.ModifiedAtUtc = DateTime.UtcNow;
      await _provider.SaveAsync();

      return true;
    }
  }
}
