using System.Collections.Generic;
using System.Web.Mvc;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.Web.Infrastructure.Binders
{
    public class CartModelBinder:IModelBinder
    {
        private const string sessioKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cart = controllerContext.HttpContext.Session[sessioKey];

            if (cart is null)
            {
                cart = new Cart() { TechnicsInCart = new List<TechnicInCart>() };
                controllerContext.HttpContext.Session[sessioKey] = cart;
            }

            return cart;
        }
    }
}