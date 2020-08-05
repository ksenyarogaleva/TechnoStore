using Microsoft.AspNet.Identity;
using TechnoStore.Common.Entities;

namespace TechnoStore.DAL.Repositories
{
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store):base(store)
        {

        }
    }
}
