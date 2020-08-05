using AutoMapper;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;

namespace TechnoStore.BLL.AutoMapper
{
    public class LogMP:Profile
    {
        public LogMP()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            CreateMap<Log, LogDTO>();
        }
    }
}
