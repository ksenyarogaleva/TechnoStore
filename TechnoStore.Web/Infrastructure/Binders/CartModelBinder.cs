using System.Web.Mvc;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.Web.Infrastructure.Binders
{
    public class CartModelBinder:IModelBinder
    {
        private const string sessioKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //get the Cart from the session
            var cart = controllerContext.HttpContext.Session[sessioKey];

            //if there was no cart in session-->create it
            if (cart is null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessioKey] = cart;
            }

            return cart;
        }
    }
}