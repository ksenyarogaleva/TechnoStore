using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string connectionString) :base(connectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
