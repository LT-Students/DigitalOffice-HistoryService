using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Models;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory
{
    public class FindServiceHistoryCommand : IFindServiceHistoryCommand
    {
        private readonly IServiceHistoryRepository _repository;
        private readonly IFindServiceHistoryResponseMapper _mapper;

        public FindServiceHistoryCommand(
            IServiceHistoryRepository repository,
            IFindServiceHistoryResponseMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public FindResponse<ServiceHistoryInfo> Execute(FindServicesHistoriesFilter filter, int skipCount, int takeCount)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
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
