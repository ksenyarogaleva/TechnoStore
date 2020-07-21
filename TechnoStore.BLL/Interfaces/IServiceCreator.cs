using TechnoStore.DAL.Context;

namespace TechnoStore.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(ApplicationContext context);
    }
}
