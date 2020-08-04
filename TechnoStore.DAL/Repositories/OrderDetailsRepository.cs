using System.Data.Entity;
using System.Threading.Tasks;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Context;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.DAL.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        protected readonly ApplicationContext context;

        public OrderDetailsRepository(ApplicationContext applicationContext)
        {
            this.context = applicationContext;
        }

        public async Task CreateAsync(OrderDetails orderDetails)
        {
            this.context.OrderDetails.Add(orderDetails);
            await this.context.SaveChangesAsync();
        }

        public async Task<OrderDetails> FindAsync(OrderDetails orderDetails)
        {
            return await this.context.OrderDetails.FirstAsync(o => o.City==orderDetails.City&&o.Country==orderDetails.Country&&o.Address==orderDetails.Address);
        }
    }
}
