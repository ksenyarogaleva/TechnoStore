using TechnoStore.BLL.Interfaces;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService()
            => new UserService(new UnitOfWork());
    }
}
