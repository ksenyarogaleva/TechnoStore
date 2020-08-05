using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Context;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.DAL.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationContext context;

        public LogRepository(ApplicationContext applicationContext)
        {
            this.context = applicationContext;
        }

        public async Task<int> CountAsync()
        {
            return await this.context.Logs.CountAsync();
        }

        public async Task DeleteAllAsync()
        {
            var logs = Task.Run(async () =>
              await this.GetAllAsync()).Result;
            this.context.Logs.RemoveRange(logs);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var log = Task.Run(async () =>
              await this.GetSingleAsync(id)).Result;
            if (log != null)
            {
                this.context.Logs.Remove(log);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            return await this.context.Logs.ToListAsync();
        }

        public async Task<Log> GetSingleAsync(int id)
        {
            return await this.context.Logs.FindAsync(id);
        }
    }
}
