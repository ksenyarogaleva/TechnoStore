using Ninject.Modules;
using TechnoStore.DAL.Interfaces;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("connectionString", "TechnoStoreDB");
        }
    }
}
