using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.HistoryService.Models.Dto.Requests.Filters
{
    public class FindServicesHistoriesFilter
    {
        [FromQuery(Name = "serviceid")]
        public Guid? ServiceId { get; set; }

        [FromQuery(Name = "version")]
        public string Verison { get; set; }
    }
}
