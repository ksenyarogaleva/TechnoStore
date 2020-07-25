using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnoStore.BLL.Infrastructure;
using TechnoStore.BLL.Interfaces;

namespace TechnoStore.Web.Infrastructure
{
    public static class KernelHolder
    {
        static StandardKernel kernel;

        public static StandardKernel Kernel
        {
            get
            {
                if (kernel == null)
                {
                    NinjectModule serviceModule = new ServiceModule("TechnoStoreDB");
                    //TODO Create ForumModule
                    kernel = new StandardKernel(serviceModule);
                }
                return kernel;
            }
        }

        public static IUserService CreateUserService()
        {
            return KernelHolder.Kernel.Get<IUserService>();
        }
    }
}