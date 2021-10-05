using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.HistoryService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ServiceController : ControllerBase
  {
    [HttpPost("create")]
    public OperationResultResponse<Guid?> Create(
      [FromServices] ICreateServiceCommand command,
      [FromBody] CreateServiceRequest request)
    {
      return command.Execute(request);
    }

    [HttpGet("find")]
    public FindResultResponse<ServiceInfo> Find(
      [FromServices] IFindServiceCommand command)
    {
      return command.Execute();
    }

    [HttpPatch("edit")]
    public OperationResultResponse<bool> Edit(
      [FromServices] IEditServiceCommand command,
      [FromQuery] Guid serviceId,
      [FromBody] JsonPatchDocument<EditServiceRequest> request)
    {
      return command.Execute(serviceId, request);
    }
  }
}
