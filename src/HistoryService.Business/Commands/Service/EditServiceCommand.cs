using FluentValidation.Results;
using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Models.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

    public async Task<OperationResultResponse<bool>> ExecuteAsync(
      Guid serviceId,
      JsonPatchDocument<EditServiceRequest> request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveHistories))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new OperationResultResponse<bool>
        {
          Status = OperationResultStatusType.Failed
        };
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);
      if (!validationResult.IsValid)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new OperationResultResponse<bool>
        {
          Status = OperationResultStatusType.Failed,
          Errors = validationResult.Errors.Select(vf => vf.ErrorMessage).ToList()
        };
      }

      OperationResultResponse<bool> response = new();

      response.Body = await _serviceRepository.EditAsync(serviceId, _mapper.Map(request));
      response.Status = OperationResultStatusType.FullSuccess;

      if (!response.Body)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

        response.Status = OperationResultStatusType.Failed;
      }

      return response;
    }
  }
}
