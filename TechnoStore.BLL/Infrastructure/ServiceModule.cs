using Ninject.Modules;
using TechnoStore.BLL.Interfaces;
using TechnoStore.BLL.Services;
using TechnoStore.DAL.Interfaces;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserService>().To<UserService>();
            Bind<ITechnicService>().To<TechnicService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IRequestService>().To<RequestService>();
            Bind<ILogService>().To<LogService>();
            Bind<ICartService>().To<CartService>();
            Bind<IOrderService>().To<OrderService>();
        }
    }
}
