using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Models;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory
{
    public class FindServiceHistoryCommand : IFindServiceHistoryCommand
    {
        private readonly IServiceHistoryRepository _repository;
        private readonly IFindServiceHistoryResponseMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public FindServiceHistoryCommand(
            IServiceHistoryRepository repository,
            IFindServiceHistoryResponseMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public FindResponse<ServiceHistoryInfo> Execute(FindServicesHistoriesFilter filter, int skipCount, int takeCount)
        {
            if (filter == null)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return new FindResponse<ServiceHistoryInfo>
                {
                    Errors = new List<string> { "Service name or version are not specified" }
                };
            }

            IEnumerable<DbServiceHistory> dbServiceHistory = _repository.Find(filter, skipCount, takeCount, out int totalCount);

            return new FindResponse<ServiceHistoryInfo>
            {
                Body = dbServiceHistory.Select(sh => _mapper.Map(sh)),
                TotalCount = totalCount
            };
        }
    }
}
