using System;

namespace LT.DigitalOffice.HistoryService.Models.Dto.Requests
{
  public class EditServiceRequest
  {
    public string Name { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTime ModifiedAtUtc { get; set; }
  }
}
