using System.Threading.Tasks;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Interfaces
{
    public interface ITechnicRepository:IRepository<Technic>
    {
        Task CreateAsync(Technic item);
        Task UpdateAsync(Technic item);
        Task DeleteAsync(Technic item);
    }
}
