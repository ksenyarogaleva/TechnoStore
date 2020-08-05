using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;
using TechnoStore.Common.Infrastructure;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        public OrderService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task ProcessOrder(Cart cart, string clientId, OrderDetailsDTO orderDetails)
        {
            var technics = cart.TechnicsInCart;
            if (technics != null)
            {
                var dtos = new List<TechnicDTO>();
                foreach(var technic in technics)
                {
                    for(var i = 0; i < technic.Quantity; i++)
                    {
                        dtos.Add(technic.Technic);
                    }
                }

                var detailsEntity = mapper.Map<OrderDetails>(orderDetails);
                var orders = new List<Order>();
                foreach (var dto in dtos)
                {
                    orders.Add(new Order() { TechnicId = dto.Id, ClientProfileId = clientId });
                }

                var entity = Task.Run(async () => await this.unitOfWork.OrderDetails.FindAsync(detailsEntity)).Result;

                if(entity is null)
                {
                    await this.unitOfWork.OrderDetails.CreateAsync(detailsEntity);
                    foreach(var order in orders)
                    {
                        order.OrderDetailsId = detailsEntity.Id;
                    }
                }
                else
                {
                    foreach (var order in orders)
                    {
                        order.OrderDetailsId = entity.Id;
                    }
                }

                foreach(var order in orders)
                {
                    await this.unitOfWork.Orders.CreateAsync(order);
                }

            }
        }

    }
}
