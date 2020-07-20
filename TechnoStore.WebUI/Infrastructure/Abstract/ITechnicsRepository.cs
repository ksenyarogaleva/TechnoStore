using System.Collections.Generic;
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
        IEnumerable<Log> Logs { get; }
        IEnumerable<RequestStatistic> RequestStatistics { get;}

        void SaveTechnics(Technic technics);
        void SaveUser(User user);
        void SaveRequest(RequestStatistic request);
        Technic DeleteTechnics(int technicsId);
        Log DeleteError(int errorId);
        void DeketeAllErrors();
    }
}
