using System.Data.Entity;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Context;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.DAL.Repositories
{
    public class TechnicRepository : Repository<Technic>, ITechnicRepository
    {

        public TechnicRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public async Task CreateAsync(Technic item)
        {
            context.Technics.Add(item);
            await base.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Technic item)
        {
            var technic = Task.Run(async () =>
            await this.GetSingleAsync(item.Id)).Result;
            if (technic != null)
            {
                base.context.Technics.Remove(technic);
                await base.context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Technic item)
        {
            base.context.Entry(item).State = EntityState.Modified;
            await base.context.SaveChangesAsync();
        }
    }
}
