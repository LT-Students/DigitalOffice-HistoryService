using FluentValidation;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Validation.Service.Interfaces
{
  [AutoInject]
  public interface IEditServiceValidator : IValidator<JsonPatchDocument<EditServiceRequest>>
  {
  }
}
