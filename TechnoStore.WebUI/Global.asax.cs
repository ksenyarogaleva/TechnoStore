using System.Web.Mvc;
using System.Web.Routing;
using TechnoStore.WebUI.Infrastructure.Binders;
using TechnoStore.WebUI.Models.Entities.Cart;

namespace TechnoStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            log4net.Config.XmlConfigurator.Configure();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
