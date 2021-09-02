using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service
{
    public class FindServiceCommand : IFindServiceCommand
    {
        private readonly IServiceRepository _repository;
        private readonly IFindServiceResponseMapper _mapper;

        public FindServiceCommand(
            IServiceRepository repository,
            IFindServiceResponseMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<ServiceInfo> Execute()
        {
            List<DbService> dbServiceList = _repository.Find();
            List<ServiceInfo> response = new List<ServiceInfo>();

            foreach (DbService dbService in dbServiceList)
            {
                response.Add(_mapper.Map(dbService));
            }

            return response;
        }
    }
}
