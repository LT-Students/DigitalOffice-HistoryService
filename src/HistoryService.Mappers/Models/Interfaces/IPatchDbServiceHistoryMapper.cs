using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using Microsoft.AspNetCore.JsonPatch;

namespace LT.DigitalOffice.HistoryService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IPatchDbServiceHistoryMapper
  {
    JsonPatchDocument<DbServiceHistory> Map(JsonPatchDocument<EditServiceHistoryRequest> request);
  }
}
