using TechnoStore.BLL.Interfaces;
using TechnoStore.DAL.Context;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(ApplicationContext context)
            => new UserService(new UnitOfWork(context));
    }
}
