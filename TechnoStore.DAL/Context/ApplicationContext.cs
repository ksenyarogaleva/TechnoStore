using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() :base("TechnoStoreDB") { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Technic> Technics { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Request> Requests { get; set; }
    }
}
