using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ServiceHistoryController : ControllerBase
  {
    [HttpPost("create")]
    public OperationResultResponse<Guid?> Create(
      [FromServices] ICreateServiceHistoryCommand command,
      [FromBody] CreateServiceHistoryRequest request)
    {
      return command.Execute(request);
    }

    [HttpGet("find")]
    public FindResultResponse<ServiceHistoryInfo> Find(
      [FromServices] IFindServiceHistoryCommand command,
      [FromQuery] FindServicesHistoriesFilter filter)
    {
      return command.Execute(filter);
    }

    [HttpPatch("edit")]
    public OperationResultResponse<bool> Edit(
      [FromServices] IEditServiceHistoryCommand command,
      [FromQuery] Guid serviceHistoryId,
      [FromBody] JsonPatchDocument<EditServiceHistoryRequest> request)
    {
      return command.Execute(serviceHistoryId, request);
    }
  }
}
