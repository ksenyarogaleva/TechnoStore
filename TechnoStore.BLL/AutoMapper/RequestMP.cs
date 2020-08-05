using AutoMapper;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;

namespace TechnoStore.BLL.AutoMapper
{
    public class RequestMP:Profile
    {
        public RequestMP()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            CreateMap<Request, RequestDTO>()
                .ReverseMap();
        }
    }
}
