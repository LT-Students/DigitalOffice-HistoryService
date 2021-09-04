using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service
{
    public class FindServiceCommand : IFindServiceCommand
    {
        private readonly IServiceRepository _repository;
        private readonly IFindServiceInfoMapper _mapper;

        public FindServiceCommand(
            IServiceRepository repository,
            IFindServiceInfoMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public OperationResultResponse<List<ServiceInfo>> Execute()
        {
            OperationResultResponse<List<ServiceInfo>> response = new();

            List<DbService> dbServiceList = _repository.Find();
 
            response.Body = dbServiceList.Select(x => _mapper.Map(x)).ToList();

            response.Status = response.Errors.Any() ? OperationResultStatusType.PartialSuccess : OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
