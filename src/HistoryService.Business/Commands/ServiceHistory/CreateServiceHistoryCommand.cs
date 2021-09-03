using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.ServiceHistory.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory
{
    public class CreateServiceHistoryCommand : ICreateServiceHistoryCommand
    {
        private readonly IServiceHistoryRepository _repository;
        private readonly IDbServiceHistoryMapper _mapperServiceHistory;
        private readonly IAccessValidator _accessValidator;
        public readonly ICreateServiceHistoryRequestValidator _validator;

        public CreateServiceHistoryCommand(
            IDbServiceHistoryMapper mapperServiceHistory,
            IServiceHistoryRepository repository,
            IAccessValidator accessValidator,
            ICreateServiceHistoryRequestValidator validator)
        {
            _repository = repository;
            _mapperServiceHistory = mapperServiceHistory;
            _accessValidator = accessValidator;
            _validator = validator;
        }

        public OperationResultResponse<Guid> Execute(CreateServiceHistoryRequest request)
        {
            if (!(_accessValidator.IsAdmin() || _accessValidator.HasRights(Rights.AddEditRemoveHistroies)))
            {
                throw new ForbiddenException("Not enough rights.");
            }

            OperationResultResponse<Guid> response = new();

            if (_repository.IsServiceHistoryVersionExist(request.Version))
            {
                response.Status = OperationResultStatusType.Failed;
                response.Errors.Add($"History version '{request.Version}' already exist");
                return response;
            }

            _validator.ValidateAndThrowCustom(request);

            DbServiceHistory dbServiceHistory = _mapperServiceHistory.Map(request);

            response.Body = _repository.Create(dbServiceHistory);

            response.Status = response.Errors.Any() ? OperationResultStatusType.PartialSuccess : OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
