using FluentValidation.Results;
using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateServiceRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveHistories))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new OperationResultResponse<Guid?>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { "Not enough rights." }
        };
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new()
        {
          Status = OperationResultStatusType.Failed,
          Errors = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList()
        };
      }

      OperationResultResponse<Guid?> response = new();

      response.Body = await _repository.CreateAsync(_mapperService.Map(request));

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
