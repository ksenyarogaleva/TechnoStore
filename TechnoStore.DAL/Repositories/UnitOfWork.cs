using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Context;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext context;
        private bool disposed = false;

        public ApplicationUserManager UserManager { get; private set; }
        public IClientManager ClientManager { get; private set; }
        public ApplicationRoleManager RoleManager { get; private set; }
        public ITechnicRepository Technics { get; private set; }
        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(string connectionString)
        {
            this.context = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(this.context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(this.context));
            ClientManager = new ClientManager(this.context);
            Technics = new TechnicRepository(this.context);
            Categories = new CategoryRepository(this.context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    ClientManager.Dispose();
                }

                this.disposed = true;
            }
        }

    }
}
