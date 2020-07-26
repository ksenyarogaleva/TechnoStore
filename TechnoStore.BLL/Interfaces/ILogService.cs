using System.Collections.Generic;
using TechnoStore.Common.DTO;

namespace TechnoStore.BLL.Interfaces
{
    public interface ILogService
    {
        void DeleteAllLogs();
        void DeleteLog(int id);
        IEnumerable<LogDTO> GetAllLogs();
        LogDTO GetLog(int id);
        int Count();
    }
}
