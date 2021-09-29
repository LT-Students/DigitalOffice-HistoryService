using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service
{
  public class CreateServiceCommand : ICreateServiceCommand
  {
    private readonly IServiceRepository _repository;
    private readonly IDbServiceMapper _mapperService;
    private readonly IAccessValidator _accessValidator;
    private readonly ICreateServiceRequestValidator _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateServiceCommand(
      IDbServiceMapper mapperService,
      IServiceRepository repository,
      IAccessValidator accessValidator,
      ICreateServiceRequestValidator validator,
      IHttpContextAccessor httpContextAccessor)
    {
      _repository = repository;
      _mapperService = mapperService;
      _accessValidator = accessValidator;
      _validator = validator;
      _httpContextAccessor = httpContextAccessor;
    }

    public OperationResultResponse<Guid?> Execute(CreateServiceRequest request)
    {
      if (!_accessValidator.IsAdmin())
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new OperationResultResponse<Guid?>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { "Not enough rights." }
        };
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new OperationResultResponse<Guid?>
        {
          Status = OperationResultStatusType.Failed,
          Errors = errors
        };
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = _repository.Create(_mapperService.Map(request));

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      response.Status = OperationResultStatusType.FullSuccess;

      if (response.Body == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        response.Status = OperationResultStatusType.Failed;
      }

      return response;
    }
  }
}
