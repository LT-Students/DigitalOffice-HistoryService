using LT.DigitalOffice.HistoryService.Business.Commands.ServiceHistory.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Dto.Models;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.HistoryService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace LT.DigitalOffice.HistoryService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceHistoryController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceHistoryController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("create")]
        public OperationResultResponse<Guid?> Create(
            [FromServices] ICreateServiceHistoryCommand command,
            [FromBody] CreateServiceHistoryRequest request)
        {
            var result = command.Execute(request);

            if (result.Status != OperationResultStatusType.Failed)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            }

            return result;
        }

        [HttpGet("find")]
        public FindResponse<ServiceHistoryInfo> Find(
           [FromServices] IFindServiceHistoryCommand command,
           [FromQuery] FindServicesHistoriesFilter filter)
        {
            return command.Execute(filter);
        }
    }
}
