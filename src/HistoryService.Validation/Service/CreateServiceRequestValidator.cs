﻿using FluentValidation;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.HistoryService.Validation.Service.Interfaces;

namespace LT.DigitalOffice.HistoryService.Validation.Service
{
    public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>, ICreateServiceRequestValidator
    {
        public CreateServiceRequestValidator()
        {
            RuleFor(service => service.Name.Trim())
                .NotEmpty()
                .WithMessage("Name cannot be empty.")
                .MaximumLength(30)
                .WithMessage("Name is too long.");
        }
    }
}