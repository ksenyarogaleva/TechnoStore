using Ninject.Modules;
using TechnoStore.BLL.Interfaces;
using TechnoStore.BLL.Services;
using TechnoStore.DAL.Interfaces;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            this.connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("connectionString", this.connectionString);
            Bind<TechnoStore.BLL.Interfaces.IUserService>().To<TechnoStore.BLL.Services.UserService>();
            Bind<ITechnicService>().To<TechnicService>();
            Bind<ICategoryService>().To<CategoryService>();
        }
    }
}
