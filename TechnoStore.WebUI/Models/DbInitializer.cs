using System.Data.Entity;
using TechnoStore.WebUI.Infrastructure.Concrete;

namespace TechnoStore.WebUI.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {

        }
    }
}