using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Infrastructure.Filters;
using TechnoStore.WebUI.Models.Entities;
using TechnoStore.WebUI.Models.Entities.Cart;

namespace TechnoStore.WebUI.Controllers
{
    [RequestStatistic]
    public class CartController : Controller
    {
        private ITechnicsRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(ITechnicsRepository technicsRepository,IOrderProcessor processor)
        {
            this.repository = technicsRepository;
            this.orderProcessor = processor;
        }

        public ActionResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl,
            });

        }

        public RedirectToRouteResult AddToCart(Cart cart, int technicsId, string returnUrl)
        {
            var technic = this.repository.Technics
                .FirstOrDefault(t => t.Id == technicsId);

            if (technic != null)
            {
                cart.AddToCart(technic, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int technicsId, string returnUrl)
        {
            var technic = this.repository.Technics
                .FirstOrDefault(t => t.Id == technicsId);

            if (technic != null)
            {
                cart.RemoveFromCart(technic);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.TechnicsInCart.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                this.orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();

                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}