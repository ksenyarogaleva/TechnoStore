using System.Data.Entity;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Context;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.DAL.Repositories
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        public RequestRepository(ApplicationContext applicationContext):base(applicationContext)
        {

        }

        public async Task CreateAsync(Request item)
        {
            context.Requests.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Request item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
