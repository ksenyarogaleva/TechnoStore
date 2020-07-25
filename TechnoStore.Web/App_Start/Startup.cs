using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using TechnoStore.BLL.Interfaces;
using TechnoStore.BLL.Services;
using TechnoStore.Web.Infrastructure;

namespace TechnoStore.Web.App_Start
{
    public class Startup
    {
        //create service for working with services
        //IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            //register service with the help of owin context
            app.CreatePerOwinContext<IUserService>(KernelHolder.CreateUserService);
            //app.CreatePerOwinContext<IUserService>(this.CreateUserService);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        //private IUserService CreateUserService()
        //    => this.serviceCreator.CreateUserService("TechnoStoreDB");
    }
}