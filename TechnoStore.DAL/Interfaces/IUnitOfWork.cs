using System;
using System.Threading.Tasks;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        ITechnicRepository Technics { get; }
        ICategoryRepository Categories { get; }
        IRequestRepository Requests { get; }
        ILogRepository Logs { get; }
        IClientManager ClientManager { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}
