using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.BLL.Interfaces
{
    public interface ICartService
    {
        void AddToCart(Cart cart,TechnicDTO technic, int quantity);
        void RemoveFromCart(Cart cart,TechnicDTO technic);
        decimal ComputeTotalValue(Cart cart);
        void Clear(Cart cart);
    }
}
