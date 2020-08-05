using AutoMapper;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;

namespace TechnoStore.BLL.AutoMapper
{
    public class TechnicMP : Profile
    {
        public TechnicMP()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            CreateMap<Technic, TechnicDTO>()
              .ForMember(x => x.Category, opt => opt.MapFrom(i => i.Category.Name));
            CreateMap<TechnicEditDTO, Technic>()
                .ForMember(x => x.Category, opt => opt.Ignore());
            CreateMap<Technic, TechnicEditDTO>()
                .ForMember(x => x.Category, opt => opt.MapFrom(i => i.Category.Name));
        }
    }
}
