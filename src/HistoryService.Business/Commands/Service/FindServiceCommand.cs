using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
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
        private readonly IAccessValidator _accessValidator;

        public FindServiceCommand(
            IServiceRepository repository,
            IFindResultResponseMapper mapper,
            IAccessValidator accessValidator,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
           _accessValidator = accessValidator;
        }

        public OperationResultResponse<List<FindResultResponse>> Execute()
        {

            if (!(_accessValidator.HasRights(Rights.AddEditRemoveHistroies)))
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                return new OperationResultResponse<List<FindResultResponse>>
                {
                  Status = OperationResultStatusType.Failed,
                  Errors = new() { "Not enough rights." }
                };
            }
            OperationResultResponse<List<FindResultResponse>> response = new();

            response.Body = _repository.Find().Select(x => _mapper.Map(x)).ToList();

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
