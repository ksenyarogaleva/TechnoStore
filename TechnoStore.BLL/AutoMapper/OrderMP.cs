using AutoMapper;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;

namespace TechnoStore.BLL.AutoMapper
{
    public class OrderMP:Profile
    {
        public OrderMP()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            CreateMap<OrderDetails, OrderDetailsDTO>()
                .ReverseMap();
        }
    }
}
