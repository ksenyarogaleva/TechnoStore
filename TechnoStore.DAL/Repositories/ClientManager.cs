using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateClient(ClientProfile client)
        {
            Database.ClientProfiles.Add(client);
            await Database.SaveChangesAsync();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task UpdateClient(ClientProfile clientProfile)
        {
            Database.Entry(clientProfile).State = EntityState.Modified;
            await Database.SaveChangesAsync();
        }

        public async Task<ClientProfile> FindAsync(string clientId)
        {
            return await Database.ClientProfiles.Where(c => c.Id == clientId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetClientOrders(string clientId)
        {
            return await Database.Orders.Where(o => o.ClientProfileId == clientId).ToListAsync();
        }
    }

}
