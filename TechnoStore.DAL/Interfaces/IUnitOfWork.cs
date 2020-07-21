using System;
using System.Threading.Tasks;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
