using System;

namespace LT.DigitalOffice.HistoryService.Models.Dto.Responses
{
  public record ServiceInfo
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; }
  }
}
