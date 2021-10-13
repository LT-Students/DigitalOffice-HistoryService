using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Provider;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<bool> DoesNameExistAsync(string name)
    {
      return await _provider.Services.AnyAsync(s => s.Name.Contains(name));
    }

    public async Task<Guid?> CreateAsync(DbService dbService)
    {
      if (dbService == null)
      {
        return null;
      }

      _provider.Services.Add(dbService);
      await _provider.SaveAsync();

      return dbService.Id;
    }

    public async Task<List<DbService>> FindAsync()
    {
      return await _provider.Services.ToListAsync();
    }

    public async Task<DbService> GetAsync(Guid serviceId)
    {
      DbService service = await _provider.Services.FirstOrDefaultAsync(e => e.Id == serviceId);
      if (service == null)
      {
        return null;
      }

      return service;
    }

    public async Task<bool> EditAsync(DbService service, JsonPatchDocument<DbService> request)
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
      await _provider.SaveAsync();

      return true;
    }
  }
}
