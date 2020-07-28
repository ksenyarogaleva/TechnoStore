using System.Web.Mvc;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.Infrastructure;
using TechnoStore.Common.ViewModels;

namespace TechnoStore.Web.Controllers
{
    public class CartController : Controller
    {
        ITechnicService service;
        ICartService cartService;

        public CartController(ITechnicService technicService,ICartService cartService)
        {
            this.service = technicService;
            this.cartService = cartService;
        }

        public ActionResult Index(Cart cart, string returnUrl)
        {
            ViewBag.TotalValue = this.cartService.ComputeTotalValue(cart);
            return View(new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl,
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int technicsId, string returnUrl)
        {
            var technic = this.service.GetSingle(technicsId);

            if (technic != null)
            {
                cartService.AddToCart(cart,technic, 1);
            }

            return RedirectToAction("Index", new { cart=cart,returnUrl=returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int technicsId, string returnUrl)
        {
            var technic = this.service.GetSingle(technicsId);

            if (technic != null)
            {
                cartService.RemoveFromCart(cart,technic);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

    }
}