using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

    public async Task<FindResultResponse<ServiceInfo>> ExecuteAsync()
    {
      if (!await _accessValidator.IsAdminAsync()||
          !await _accessValidator.HasRightsAsync(Rights.AddEditRemoveHistories))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new FindResultResponse<ServiceInfo>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { "Not enough rights." }
        };
      }

      FindResultResponse<ServiceInfo> response = new();

      List<DbService> dbServiceList = await _repository.FindAsync();

      response.Body = dbServiceList.Select(dbService => _mapper.Map(dbService)).ToList();

      response.Status = OperationResultStatusType.FullSuccess;

      return response;
    }
  }
}
