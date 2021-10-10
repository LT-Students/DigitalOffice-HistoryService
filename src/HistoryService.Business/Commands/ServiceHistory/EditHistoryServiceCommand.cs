using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Models.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.ServiceHistory.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory
{
  public class EditServiceHistoryCommand : IEditServiceHistoryCommand
  {
    private readonly IServiceHistoryRepository _repository;
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPatchDbServiceHistoryMapper _mapper;
    private readonly IEditServiceHistoryValidator _validator;

    public EditServiceHistoryCommand(
      IServiceHistoryRepository repository,
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IPatchDbServiceHistoryMapper mapper,
      IEditServiceHistoryValidator validator)
    {
      _repository = repository;
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _validator = validator;
    }

    public OperationResultResponse<bool> Execute(
      Guid serviceHistoryId,
      JsonPatchDocument<EditServiceHistoryRequest> request)
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

      DbServiceHistory service = _repository.Get(serviceHistoryId);
      if (service == null)
      {
        return new OperationResultResponse<bool>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { $"Service with this Id: '{serviceHistoryId}' doesn't exist" }
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

      bool result = _repository.Edit(service, _mapper.Map(request));

      return new OperationResultResponse<bool>
      {
        Status = OperationResultStatusType.FullSuccess,
        Body = result
      };
    }
  }
}
