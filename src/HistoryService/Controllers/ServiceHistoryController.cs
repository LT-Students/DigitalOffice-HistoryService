using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
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
  public class ServiceHistoryController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<Guid?>> CreateAsync(
      [FromServices] ICreateServiceHistoryCommand command,
      [FromBody] CreateServiceHistoryRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<ServiceHistoryInfo>> FindAsync(
      [FromServices] IFindServiceHistoryCommand command,
      [FromQuery] FindServicesHistoriesFilter filter)
    {
      return await command.ExecuteAsync(filter);
    }

    [HttpPatch("edit")]
    public async Task<OperationResultResponse<bool>> EditAsync(
      [FromServices] IEditServiceHistoryCommand command,
      [FromQuery] Guid serviceHistoryId,
      [FromBody] JsonPatchDocument<EditServiceHistoryRequest> request)
    {
      return await command.ExecuteAsync(serviceHistoryId, request);
    }
  }
}
