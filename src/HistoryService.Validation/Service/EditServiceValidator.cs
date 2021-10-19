﻿using FluentValidation;
using FluentValidation.Validators;
using LT.DigitalOffice.HistoryService.Data.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;
using LT.DigitalOffice.Kernel.Validators;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.HistoryService.Validation.Service
{
  public class EditServiceValidator : BaseEditRequestValidator<EditServiceRequest>, IEditServiceValidator
  {
    private readonly IServiceRepository _repository;

    private void HandleInternalPropertyValidation(Operation<EditServiceRequest> requestedOperation, CustomContext context)
    {
      Context = context;
      RequestedOperation = requestedOperation;

      #region local functions

      AddСorrectPaths(
        new List<string>
        {
          nameof(EditServiceRequest.Name)
        });

      AddСorrectOperations(nameof(EditServiceRequest.Name), new List<OperationType> { OperationType.Replace });

      AddFailureForPropertyIf(
        nameof(EditServiceRequest.Name),
        o => o == OperationType.Replace,
        new Dictionary<Func<Operation<EditServiceRequest>, bool>, string>
        {
          { x => !string.IsNullOrEmpty(x.value?.ToString().Trim()), "Name can't be empty"},
          { x => x.value.ToString().Trim().Length < 30, "Name is too long."}
        }, CascadeMode.Stop);

      AddFailureForPropertyIfAsync(
        nameof(EditServiceRequest.Name),
        x => x == OperationType.Replace,
        new()
        {
          { async x => !await _repository.DoesNameExistAsync(x.value.ToString()), "The name already exist." }
        });
      #endregion
    }

    public EditServiceValidator()
    {
      RuleForEach(x => x.Operations)
        .Custom(HandleInternalPropertyValidation);
    }
  }
}
