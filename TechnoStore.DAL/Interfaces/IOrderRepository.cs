using System.Threading.Tasks;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
    }
}
