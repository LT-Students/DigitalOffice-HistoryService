using System;

namespace LT.DigitalOffice.HistoryService.Models.Dto.Requests
{
  public class EditServiceHistoryRequest
  {
    public Guid ServiceId { get; set; }
    public string Content { get; set; }
    public string Version { get; set; }
  }
}
