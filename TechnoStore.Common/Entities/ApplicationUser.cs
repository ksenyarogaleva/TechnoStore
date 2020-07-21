using Microsoft.AspNet.Identity.EntityFramework;

namespace TechnoStore.Common.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
