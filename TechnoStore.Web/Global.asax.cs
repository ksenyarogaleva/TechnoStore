using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;
using TechnoStore.Common.Infrastructure;
using TechnoStore.Web.Infrastructure;
using TechnoStore.Web.Infrastructure.Binders;

namespace TechnoStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(KernelHolder.Kernel));
        }

    }
}
