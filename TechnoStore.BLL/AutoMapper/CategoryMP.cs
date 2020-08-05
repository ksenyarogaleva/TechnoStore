using AutoMapper;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;

namespace TechnoStore.BLL.AutoMapper
{
    public class CategoryMP:Profile
    {
        public CategoryMP()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();
        }
    }
}
