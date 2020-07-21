using Microsoft.AspNet.Identity;
using TechnoStore.Common.Entities;

namespace TechnoStore.Common.Identity
{
    public class ApplicationUserManager:UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store):base(store)
        {

        }
    }
}
