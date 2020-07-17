using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoStore.WebUI.Models.Entities.Cart
{
    public class Cart
    {
        private List<CartTechnic> technicsInCart = new List<CartTechnic>();

        public IEnumerable<CartTechnic> TechnicsInCart { get { return this.technicsInCart; } }

        public void AddToCart(Technic technic, int quantity)
        {
            var cartTechnic = this.technicsInCart
                .Where(c => c.Technic.Id == technic.Id)
                .FirstOrDefault();

            if(cartTechnic is null)
            {
                this.technicsInCart.Add(new CartTechnic
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

        public void RemoveFromCart(Technic technic)
        {
            this.technicsInCart.RemoveAll(t => t.Technic.Id == technic.Id);
        }

        public decimal ComputeTotalValue()
        {
            return this.technicsInCart.Sum(t => t.Technic.Price * t.Quantity);
        }

        public void Clear()
        {
            this.technicsInCart.Clear();
        }
    }
}