using FluentValidation;
using FluentValidation.Validators;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.HistoryService.Validation.Service
{
  public class EditServiceValidator : AbstractValidator<JsonPatchDocument<EditServiceRequest>>, IEditServiceValidator
  {
    private void HandleInternalPropertyValidation(Operation<EditServiceRequest> requestedOperation, CustomContext context)
    {
      #region local functions

      void AddСorrectPaths(List<string> paths)
      {
        if (paths.FirstOrDefault(p => p.EndsWith(requestedOperation.path[1..], StringComparison.OrdinalIgnoreCase)) == null)
        {
          context.AddFailure(requestedOperation.path, $"This path {requestedOperation.path} is not available");
        }
      }

      void AddСorrectOperations(
        string propertyName,
        List<OperationType> types)
      {
        if (requestedOperation.path.EndsWith(propertyName, StringComparison.OrdinalIgnoreCase)
            && !types.Contains(requestedOperation.OperationType))
        {
          context.AddFailure(propertyName, $"This operation {requestedOperation.OperationType} is prohibited for {propertyName}");
        }
      }

      void AddFailureForPropertyIf(
        string propertyName,
        Func<OperationType, bool> type,
        Dictionary<Func<Operation<EditServiceRequest>, bool>, string> predicates)
      {
        if (!requestedOperation.path.EndsWith(propertyName, StringComparison.OrdinalIgnoreCase)
            || !type(requestedOperation.OperationType))
        {
          return;
        }

        foreach (var validateDelegate in predicates)
        {
          if (!validateDelegate.Key(requestedOperation))
          {
            context.AddFailure(propertyName, validateDelegate.Value);
          }
        }
      }

      #endregion

      #region paths

      AddСorrectPaths(
        new List<string>
        {
          nameof(EditServiceRequest.Name)
        });

      AddСorrectOperations(nameof(EditServiceRequest.Name), new List<OperationType> { OperationType.Replace });

      #endregion

      AddFailureForPropertyIf(
        nameof(EditServiceRequest.Name),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditServiceRequest>, bool>, string>
        {
          { x => !string.IsNullOrEmpty(x.value?.ToString()), "Name can't be empty"},
          { x => x.value.ToString().Length < 30, "Name is too long."}
        });
    }

    public EditServiceValidator()
    {
      RuleForEach(x => x.Operations)
        .Custom(HandleInternalPropertyValidation);
    }
  }
}
