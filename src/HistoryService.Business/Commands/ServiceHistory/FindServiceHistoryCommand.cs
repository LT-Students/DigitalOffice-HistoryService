using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Models;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LT.DigitalOffice.Kernel.Validators.Interfaces;


namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory
{
  public class FindServiceHistoryCommand : IFindServiceHistoryCommand
    {
        private readonly IServiceHistoryRepository _repository;
        private readonly IFindServiceHistoryResponseMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccessValidator _accessValidator;
        private readonly IBaseFindRequestValidator _validator;

        public FindServiceHistoryCommand(
            IServiceHistoryRepository repository,
            IFindServiceHistoryResponseMapper mapper,
            IAccessValidator accessValidator,
            IBaseFindRequestValidator validator,
            IHttpContextAccessor httpContextAccessor)
        {
            _accessValidator = accessValidator;
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
        }

        public FindResponse<ServiceHistoryInfo> Execute(FindServicesHistoriesFilter filter)
        {
            if (!(_accessValidator.HasRights(Rights.AddEditRemoveHistories)))
            {
              _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

              return new FindResponse<ServiceHistoryInfo>
              {
                Errors = new List<string> { "Not enough rights." }
              };
            }

            if (filter == null)
            {
              _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

              return new FindResponse<ServiceHistoryInfo>
              {
                  Errors = new List<string> { "Service name or version are not specified" }
              };
            }

            if (!_validator.ValidateCustom(filter, out List<string> errors))
            {
              _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

              return new FindResponse<ServiceHistoryInfo>
              {
                Errors = errors
              };
            }

            IEnumerable<DbServiceHistory> dbServiceHistory = _repository.Find(filter, out int totalCount);

            return new FindResponse<ServiceHistoryInfo>
            {
                Body = dbServiceHistory.Select(sh => _mapper.Map(sh)),
                TotalCount = totalCount
            };
        }
    }
}
