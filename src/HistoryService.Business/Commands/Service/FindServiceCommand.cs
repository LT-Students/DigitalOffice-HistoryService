using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service
{
    public class FindServiceCommand : IFindServiceCommand
    {
        private readonly IServiceRepository _repository;
        private readonly IFindResultResponseMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FindServiceCommand(
            IServiceRepository repository,
            IFindResultResponseMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public OperationResultResponse<List<FindResultResponse>> Execute()
        {
            OperationResultResponse<List<FindResultResponse>> response = new();

            List<DbService> dbServiceList = _repository.Find();

            response.Body = dbServiceList.Select(x => _mapper.Map(x)).ToList();

            if (response.Body == null)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;

                response.Status = OperationResultStatusType.Failed;
                response.Errors.Add("Services are not exist");
                return response;
            }

            response.Status = OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
