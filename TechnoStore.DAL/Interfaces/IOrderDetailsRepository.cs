using System.Threading.Tasks;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Interfaces
{
    public interface IOrderDetailsRepository
    {
        Task CreateAsync(OrderDetails orderDetails);
        Task<OrderDetails> FindAsync(OrderDetails orderDetails);
    }
}
