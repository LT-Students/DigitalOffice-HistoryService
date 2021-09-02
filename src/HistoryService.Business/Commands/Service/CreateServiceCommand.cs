using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Business.Commands.Service
{
    public class CreateServiceCommand : ICreateServiceCommand
    {
        private readonly IServiceRepository _repository;
        private readonly IDbServiceMapper _mapperService;
        private readonly IAccessValidator _accessValidator;
        public readonly ICreateServiceRequestValidator _validator;

        public CreateServiceCommand(
            IDbServiceMapper mapperService,
            IServiceRepository repository,
            IAccessValidator accessValidator,
            ICreateServiceRequestValidator validator)
        {
            _repository = repository;
            _mapperService = mapperService;
            _accessValidator = accessValidator;
            _validator = validator;
        }

        public OperationResultResponse<Guid> Execute(CreateServiceRequest request)
        {
            if (!(_accessValidator.IsAdmin() || _accessValidator.HasRights(Rights.AddEditRemoveHistroies)))
            {
                throw new ForbiddenException("Not enough rights.");
            }

            OperationResultResponse<Guid> response = new();

            if (_repository.IsServiceNameExist(request.Name))
            {
                response.Status = OperationResultStatusType.Failed;
                response.Errors.Add($"Service with name '{request.Name}' already exist");
                return response;
            }

            _validator.ValidateAndThrowCustom(request);

            DbService dbService = _mapperService.Map(request);

            response.Body = _repository.Create(dbService);

            response.Status = response.Errors.Any() ? OperationResultStatusType.PartialSuccess : OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
