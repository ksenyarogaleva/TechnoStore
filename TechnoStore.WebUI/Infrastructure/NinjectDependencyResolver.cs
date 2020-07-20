using Moq;
using Ninject;
using Ninject.Web.Mvc.FilterBindingSyntax;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Infrastructure.Concrete;
using TechnoStore.WebUI.Infrastructure.Filters;

namespace TechnoStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //repositoy creation
            this.kernel.Bind<ITechnicsRepository>().To<EFTechnicsRepository>()
                .WithConstructorArgument("dbContext", new EFDbContext());


            //order processing creation
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            this.kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}