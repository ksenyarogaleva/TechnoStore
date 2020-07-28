using System.Collections.Generic;
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
            if (cart.TechnicsInCart is null)
            {
                cart.TechnicsInCart = new List<TechnicInCart>();
            }
            var cartTechnic = cart.TechnicsInCart
                .Where(c => c.Technic.Id == technic.Id)
                .FirstOrDefault();

            if (cartTechnic is null)
            {
                cartTechnic = new TechnicInCart
                {
                    Technic = technic,
                    Quantity = quantity,
                };

                cart.TechnicsInCart.Add(cartTechnic);
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
        {
            return cart.TechnicsInCart.Sum(t => t.Technic.Price * t.Quantity);
        }



        public void RemoveFromCart(Cart cart, TechnicDTO technic)
        {
            cart.TechnicsInCart.RemoveAll(t => t.Technic.Id == technic.Id);
        }
    }
}
