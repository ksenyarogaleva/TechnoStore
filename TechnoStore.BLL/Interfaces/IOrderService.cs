using System.Collections;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.BLL.Interfaces
{
    public interface IOrderService
    {
        void ProcessOrder(Cart cart, string clientId, OrderDetailsDTO orderDetails);
    }
}
