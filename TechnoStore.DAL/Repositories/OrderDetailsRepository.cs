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
    }
}
