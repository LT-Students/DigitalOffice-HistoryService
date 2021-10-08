using LT.DigitalOffice.Kernel.Validators.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters
{
  public record FindServicesHistoriesFilter : BaseFindRequest
  {
    [FromQuery(Name = "serviceid")]
    public Guid? ServiceId { get; set; }

    [FromQuery(Name = "version")]
    public string Verison { get; set; }
  }
}
