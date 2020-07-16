
using System.Data.Entity;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Abstract
{
    /// <summary>
    /// Represets abstract storage for getting sequence of technic products.
    /// </summary>
    public interface ITechnicsRepository
    {
        DbSet<Technics> Technics { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }

        void SaveTechnics(Technics technics);
        void SaveUser(User user);
        Technics DeleteTechnics(int technicsId);
    }
}
