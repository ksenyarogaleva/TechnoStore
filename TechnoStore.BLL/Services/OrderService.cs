using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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

        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void ProcessOrder(Cart cart, string clientId, OrderDetailsDTO orderDetails)
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

                var mapper = this.GetMapper();
                var detailsEntity = mapper.Map<OrderDetails>(orderDetails);
                var order = new Order()
                {
                    ClientProfileId = clientId,
                };

                detailsEntity.Order = order;
                foreach(var dto in dtos)
                {
                    order.Technics.Add(this.ConvertDTOIntoEntity(dto));
                }
                order.OrderDetails = detailsEntity;

                
            }
        }

        private IMapper GetMapper()
        {
            return new MapperConfiguration(c =>
            {
                c.CreateMap<OrderDetailsDTO, OrderDetails>();
                c.CreateMap<OrderDetails, OrderDetailsDTO>();
            }).CreateMapper();
        }

        private Technic ConvertDTOIntoEntity(TechnicDTO technic)
        {
            return new Technic
            {
                Id = technic.Id,
                Name = technic.Name,
                Description = technic.Description,
                Price = technic.Price,
                CategoryId = Task.Run(async () => await this.unitOfWork.Categories.FindAsync(cat => cat.Name.Equals(technic.Category))).Result.First().Id,
            };
        }
    }
}
