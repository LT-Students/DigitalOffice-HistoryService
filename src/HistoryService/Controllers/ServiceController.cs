using LT.DigitalOffice.HistoryService.Business.Commands.Service.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class ServiceController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateServiceCommand command,
      [FromBody] CreateServiceRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<ServiceInfo>> FindAsync(
      [FromServices] IFindServiceCommand command)
    {
      return await command.ExecuteAsync();
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditServiceCommand command,
      [FromQuery] Guid serviceId,
      [FromBody] JsonPatchDocument<EditServiceRequest> request)
    {
      return await command.ExecuteAsync(serviceId, request);
    }
  }
}
