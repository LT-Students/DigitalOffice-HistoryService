using System;

namespace LT.DigitalOffice.HistoryService.Models.Dto.Responses
{
  public record ServiceHistoryInfo
  {
    public Guid Id { get; set; }
    public ServiceInfo Service { get; set; }
    public string Version { get; set; }
    public string Content { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
  }
}
