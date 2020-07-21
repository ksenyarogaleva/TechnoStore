using TechnoStore.Common.Entities;
using TechnoStore.DAL.Context;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void CreateClient(ClientProfile client)
        {
            Database.ClientProfiles.Add(client);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }

}
