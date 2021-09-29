using LT.DigitalOffice.HistoryService.Models.Db;
using LT.DigitalOffice.HistoryService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.HistoryService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbServiceHistoryMapper
  {
    DbServiceHistory Map(CreateServiceHistoryRequest request);
  }
}
