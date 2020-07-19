
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Infrastructure.Abstract
{
    /// <summary>
    /// Represets abstract storage for getting sequence of technic products.
    /// </summary>
    public interface ITechnicsRepository
    {
        IEnumerable<Technic> Technics { get; }
        IEnumerable<Category> Categories { get; }
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }
        IEnumerable<ExceptionDetail> Exceptions { get; }

        void SaveTechnics(Technic technics);
        void SaveUser(User user);
        Technic DeleteTechnics(int technicsId);
    }
}
