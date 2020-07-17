using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Entities.Cart;

namespace TechnoStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ITechnicsRepository repository;

        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = this.GetCart(),
                ReturnUrl=returnUrl,
            });
            
        }

        public CartController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }

        public RedirectToRouteResult AddToCart(int technicsId,string returnUrl)
        {
            var technic = this.repository.Technics
                .FirstOrDefault(t => t.Id == technicsId);

            if (technic != null)
            {
                this.GetCart().AddToCart(technic, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int technicsId,string returnUrl)
        {
            var technic = this.repository.Technics
                .FirstOrDefault(t => t.Id == technicsId);

            if (technic != null)
            {
                this.GetCart().RemoveFromCart(technic);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if(cart is null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}