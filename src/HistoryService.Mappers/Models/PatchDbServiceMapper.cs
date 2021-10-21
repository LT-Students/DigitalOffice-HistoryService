using LT.DigitalOffice.HistoryService.Mappers.Models.Interfaces;
using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace LT.DigitalOffice.HistoryService.Mappers.Models
{
  public class PatchDbServiceMapper : IPatchDbServiceMapper
  {
    public JsonPatchDocument<DbService> Map(JsonPatchDocument<EditServiceRequest> request)
    {
      if (request == null)
      {
        return null;
      }

      JsonPatchDocument<DbService> dbService = new();

      foreach (var item in request.Operations)
      {
        dbService.Operations.Add(new Operation<DbService>(item.op, item.path, item.from, item.value));
      }

      return dbService;
    }
  }
}
