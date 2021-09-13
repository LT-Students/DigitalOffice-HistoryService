using LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace LT.DigitalOffice.HistoryService.Mappers.Db
{
    public class DbServiceMapper : IDbServiceMapper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbServiceMapper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbService Map(CreateServiceRequest request)
        {
            if (request == null)
            {
                return null;
            }

            return new DbService
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
                CreatedAtUtc = DateTime.UtcNow
            };
        }
    }
}
