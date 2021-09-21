using LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace LT.DigitalOffice.HistoryService.Mappers.Db
{
    public class DbServiceHistoryMapper : IDbServiceHistoryMapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbServiceHistoryMapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbServiceHistory Map(CreateServiceHistoryRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new DbServiceHistory
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                Version = request.Version.Trim(),
                ServiceId = request.ServiceId,
                CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
                CreatedAtUtc = DateTime.UtcNow
            };
        }
    }
}
