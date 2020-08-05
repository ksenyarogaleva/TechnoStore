using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TechnoStore.BLL.Interfaces;
using TechnoStore.BLL.Services;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.Tests
{
    [TestFixture]
    public class CartServiceTests
    {
        [Test]
        public void ComputeTotalValueTest()
        {

            var technic = new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M };
            var quantity = 2;
            var cart = new Cart
            {
                TechnicsInCart = new List<TechnicInCart>
            {
                new TechnicInCart{ Technic=technic,Quantity=quantity}
            }
            };
            ICartService cartService = new CartService();

            var actualResult = cartService.ComputeTotalValue(cart);
            var expectedResult = cart.TechnicsInCart.Sum(t => t.Technic.Price * t.Quantity);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AddToCartTest()
        {
            var technic1 = new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M };
            var technic2 = new TechnicDTO { Id = 2, Category = "Phones", Description = "Cool phone", Name = "IPhone 5s", Price = 250.99M };
            var cart = new Cart
            {
                TechnicsInCart = new List<TechnicInCart>
                {
                    new TechnicInCart { Technic = technic1, Quantity = 2 },
                    new TechnicInCart { Technic = technic2, Quantity = 1 },
                }
            };
            ICartService cartService = new CartService();

            cartService.AddToCart(cart, technic1, 10);

            Assert.AreEqual(2, cart.TechnicsInCart.Count);
            Assert.AreEqual(12, cart.TechnicsInCart.First(t => t.Technic.Id == technic1.Id).Quantity);

        }

        [Test]
        public void ComputeTotalValueTest_EmptyCart()
        {
            var cart = new Cart();
            ICartService cartService = new CartService();

            var result=cartService.ComputeTotalValue(cart);

            Assert.AreEqual(0, result);
            Assert.AreEqual(null, cart.TechnicsInCart);

        }

        [Test]
        public void RemoveFromCart()
        {
            var technic1 = new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M };
            var technic2 = new TechnicDTO { Id = 2, Category = "Phones", Description = "Cool phone", Name = "IPhone 5s", Price = 250.99M };
            var cart = new Cart
            {
                TechnicsInCart = new List<TechnicInCart>
                {
                    new TechnicInCart { Technic = technic1, Quantity = 13 },
                    new TechnicInCart { Technic = technic2, Quantity = 1 },
                }
            };
            ICartService cartService = new CartService();

            cartService.RemoveFromCart(cart, technic1);

            Assert.AreEqual(1, cart.TechnicsInCart.Count);
            Assert.AreEqual(null, cart.TechnicsInCart.Find(t => t.Technic.Id == technic1.Id));
        }
    }
}
