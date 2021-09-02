using System;

namespace LT.DigitalOffice.HistoryService.Models.Dto.Requests
{
    public record CreateServiceHistoryRequest
    {
        public Guid ServiceId { get; set; }
        public string Version { get; set; }
        public string Content { get; set; }
    }
}
