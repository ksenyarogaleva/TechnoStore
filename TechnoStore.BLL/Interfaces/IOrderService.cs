using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.BLL.Interfaces
{
    public interface IOrderService
    {
        Task ProcessOrder(Cart cart, string clientId, OrderDetailsDTO orderDetails);
    }
}
