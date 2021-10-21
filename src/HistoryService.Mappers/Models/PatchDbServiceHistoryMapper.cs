using LT.DigitalOffice.HistoryService.Mappers.Models.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace LT.DigitalOffice.HistoryService.Mappers.Models
{
  public class PatchDbServiceHistoryMapper : IPatchDbServiceHistoryMapper
  {
    public JsonPatchDocument<DbServiceHistory> Map(JsonPatchDocument<EditServiceHistoryRequest> request)
    {
      if (request == null)
      {
        return null;
      }

      JsonPatchDocument<DbServiceHistory> dbServiceHistory = new();

      foreach (var item in request.Operations)
      {
        dbServiceHistory.Operations.Add(new Operation<DbServiceHistory>(item.op, item.path, item.from, item.value));
      }

      return dbServiceHistory;
    }
  }
}
