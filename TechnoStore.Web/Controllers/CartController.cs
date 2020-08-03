using Microsoft.AspNet.Identity;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Mvc;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;
using TechnoStore.Common.ViewModels;

namespace TechnoStore.Web.Controllers
{
    public class CartController : Controller
    {
        ITechnicService service;
        ICartService cartService;
        IOrderService orderService;

        public CartController(ITechnicService technicService, ICartService cartService, IOrderService orderService)
        {
            this.service = technicService;
            this.cartService = cartService;
            this.orderService = orderService;
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
                cartService.AddToCart(cart, technic, 1);
            }

            return RedirectToAction("Index", new { cart = cart, returnUrl = returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int technicsId, string returnUrl)
        {
            var technic = this.service.GetSingle(technicsId);

            if (technic != null)
            {
                cartService.RemoveFromCart(cart, technic);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult Checkout()
        {
            return View(new OrderDetailsDTO());
        }

        [HttpPost]
        public RedirectToRouteResult Checkout(Cart cart, OrderDetailsDTO orderDetails)
        {
            var userId = User.Identity.GetUserId();
            this.orderService.ProcessOrder(cart, userId, orderDetails);
            return RedirectToAction("OrderList", "Account");
        }
    }
}