using System.Linq;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.BLL.Services
{
    public class CartService : ICartService
    {
        public void AddToCart(Cart cart, TechnicDTO technic, int quantity)
        {
            var cartTechnic = cart.TechnicsInCart
                .Where(c => c.Technic.Id == technic.Id)
                .FirstOrDefault();

            if (cartTechnic is null)
            {
                cart.TechnicsInCart.ToList().Add(new TechnicInCart
                {
                    Technic = technic,
                    Quantity = quantity,
                });
            }
            else
            {
                cartTechnic.Quantity += quantity;
            }
        }

        public void Clear(Cart cart)
        {
            cart.TechnicsInCart.ToList().Clear();
        }

        public decimal ComputeTotalValue(Cart cart)
            => cart.TechnicsInCart.Sum(t => t.Technic.Price * t.Quantity);

        public void RemoveFromCart(Cart cart, TechnicDTO technic)
        {
            cart.TechnicsInCart.ToList().RemoveAll(t => t.Technic.Id == technic.Id);
        }
    }
}
