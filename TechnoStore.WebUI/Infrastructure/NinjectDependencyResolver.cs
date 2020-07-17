using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Infrastructure.Concrete;

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
            //    // Имитированная реализация ITechnicsRepository, которая будет замещать хранилище данных до тех пор,пока не дойду руки до него
            //    Mock<ITechnicsRepository> mock = new Mock<ITechnicsRepository>();
            //    mock.Setup(t => t.Technics).Returns(new List<Technics>
            //    {
            //        new Technics{Name="Apple IPhone 7",Price=500.5m},
            //        new Technics{Name="Apple MacBook Pro 15'",Price=1499},
            //        new Technics{Name="Apple AirPods",Price=129.99m}

            //    });

            //repositoy creation
            this.kernel.Bind<ITechnicsRepository>().To<EFTechnicsRepository>();

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