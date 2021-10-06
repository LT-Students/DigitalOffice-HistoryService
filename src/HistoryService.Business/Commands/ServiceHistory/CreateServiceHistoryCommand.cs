using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.ServiceHistory.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory
{
  public class CreateServiceHistoryCommand : ICreateServiceHistoryCommand
  {
    private readonly IServiceHistoryRepository _repository;
    private readonly IDbServiceHistoryMapper _mapperServiceHistory;
    private readonly IAccessValidator _accessValidator;
    private readonly ICreateServiceHistoryRequestValidator _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateServiceHistoryCommand(
      IDbServiceHistoryMapper mapperServiceHistory,
      IServiceHistoryRepository repository,
      IAccessValidator accessValidator,
      ICreateServiceHistoryRequestValidator validator,
      IHttpContextAccessor httpContextAccessor)
    {
      _repository = repository;
      _mapperServiceHistory = mapperServiceHistory;
      _accessValidator = accessValidator;
      _validator = validator;
      _httpContextAccessor = httpContextAccessor;
    }

    public OperationResultResponse<Guid?> Execute(CreateServiceHistoryRequest request)
    {
      if (!_accessValidator.IsAdmin()||
        _accessValidator.HasRights(Rights.AddEditRemoveHistories))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new OperationResultResponse<Guid?>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { "Not enough rights." }
        };
      }

      OperationResultResponse<Guid?> response = new();

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new OperationResultResponse<Guid?>
        {
          Status = OperationResultStatusType.Failed,
          Errors = errors
        };
      }

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      response.Status = OperationResultStatusType.FullSuccess;

      response.Body = _repository.Create(_mapperServiceHistory.Map(request));

      if (response.Body == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        response.Status = OperationResultStatusType.Failed;
      }

      return response;
    }
  }
}
