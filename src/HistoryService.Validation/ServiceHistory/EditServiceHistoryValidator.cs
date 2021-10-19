using FluentValidation;
using FluentValidation.Validators;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.ServiceHistory.Interfaces;
using LT.DigitalOffice.Kernel.Validators;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Validation.ServiceHistory
{
  public class EditServiceHistoryValidator : BaseEditRequestValidator<EditServiceHistoryRequest>, IEditServiceHistoryValidator
  {
    private void HandleInternalPropertyValidation(Operation<EditServiceHistoryRequest> reqrequestedOperationuest, CustomContext context)
    {
      Context = context;
      RequestedOperation = reqrequestedOperationuest;

      #region local functions

      AddСorrectPaths(
        new List<string>
        {
          nameof(EditServiceHistoryRequest.Content),
          nameof(EditServiceHistoryRequest.Version),
          nameof(EditServiceHistoryRequest.ServiceId)
        });

      AddСorrectOperations(nameof(EditServiceHistoryRequest.Content), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditServiceHistoryRequest.Version), new List<OperationType> { OperationType.Replace });
      AddСorrectOperations(nameof(EditServiceHistoryRequest.ServiceId), new List<OperationType> { OperationType.Replace });

      AddFailureForPropertyIf(
        nameof(EditServiceHistoryRequest.Content),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditServiceHistoryRequest>, bool>, string>
        {
          { x => !string.IsNullOrEmpty(x.value?.ToString().Trim()), "Content can't be empty"}
        });

      AddFailureForPropertyIf(
        nameof(EditServiceHistoryRequest.ServiceId),
        o => o == OperationType.Replace,
        new()
        {
          { x => Guid.TryParse(x.value?.ToString(), out Guid result), "Service id has incorrect format." },
        });

      AddFailureForPropertyIf(
        nameof(EditServiceHistoryRequest.Version),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditServiceHistoryRequest>, bool>, string>
        {
          { x  => !string.IsNullOrEmpty(x.value?.ToString().Trim()), "Version can't be empty"}
        });

      #endregion
    }

    public EditServiceHistoryValidator()
    {
      RuleForEach(x => x.Operations)
        .Custom(HandleInternalPropertyValidation);
    }
  }
}
