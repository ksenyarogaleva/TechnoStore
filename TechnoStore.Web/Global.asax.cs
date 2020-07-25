using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;
using TechnoStore.Web.Infrastructure;

namespace TechnoStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(KernelHolder.Kernel));
        }
    }
}
