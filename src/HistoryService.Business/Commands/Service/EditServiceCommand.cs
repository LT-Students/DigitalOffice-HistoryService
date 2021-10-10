﻿using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Models.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service
{
  public class EditServiceCommand : IEditServiceCommand
  {
    private readonly IServiceRepository _serviceRepository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPatchDbServiceMapper _mapper;
    private readonly IEditServiceValidator _validator;

    public EditServiceCommand(
      IServiceRepository serviceRepository,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IPatchDbServiceMapper mapper,
      IEditServiceValidator validator)
    {
      _serviceRepository = serviceRepository;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _validator = validator;
    }

    public OperationResultResponse<bool> Execute(
      Guid serviceId,
      JsonPatchDocument<EditServiceRequest> request)
    {
      if (!_accessValidator.IsAdmin())
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new OperationResultResponse<bool>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { "Not enough rights." }
        };
      }

      DbService service = _serviceRepository.Get(serviceId);
      if (service == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

        return new OperationResultResponse<bool>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { $"Service with this Id: '{serviceId}' doesn't exist" }
        };
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new OperationResultResponse<bool>
        {
          Status = OperationResultStatusType.Failed,
          Errors = errors
        };
      }

      bool result = _serviceRepository.Edit(service, _mapper.Map(request));

      return new OperationResultResponse<bool>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = result
      };
    }
  }
}
