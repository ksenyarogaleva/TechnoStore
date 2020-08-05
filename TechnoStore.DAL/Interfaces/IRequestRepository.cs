using System.Threading.Tasks;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Interfaces
{
    public interface IRequestRepository:IRepository<Request>
    {
        Task CreateAsync(Request item);
        Task UpdateAsync(Request item);
    }
}
