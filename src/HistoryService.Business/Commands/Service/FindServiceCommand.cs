using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
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

    public FindResultResponse<ServiceInfo> Execute()
    {
      if (!_accessValidator.IsAdmin())
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new FindResultResponse<ServiceInfo>
        {
            Status = OperationResultStatusType.Failed,
            Errors = new() { "Not enough rights." }
        };
      }

      FindResultResponse<ServiceInfo> response = new();

      response.Body = _repository.Find().Select(_mapper.Map).ToList();

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Found;

      response.Status = OperationResultStatusType.FullSuccess;

      if (response.Body == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;

        response.Status = OperationResultStatusType.Failed;
        return response;
      }

      return response;
    }
  }
}
