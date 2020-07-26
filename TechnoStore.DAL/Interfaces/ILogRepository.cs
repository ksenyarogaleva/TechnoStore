using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Interfaces
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAllAsync();
        Task<Log> GetSingleAsync(int id);
        Task DeleteAsync(int id);
        Task DeleteAllAsync();
        Task<int> CountAsync();
    }
}
