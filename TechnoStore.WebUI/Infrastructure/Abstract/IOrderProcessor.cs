using TechnoStore.WebUI.Models.Entities;
using TechnoStore.WebUI.Models.Entities.Cart;

namespace TechnoStore.WebUI.Infrastructure.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }

}
