using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

namespace TechnoStore.BLL.AutoMapper
{
    public class AutoMapperConfiguration
    {

        public static IMapper GetMapperConfiguration()
        {
            var config = new MapperConfiguration(x =>
              {
                  x.AddProfile(new TechnicMP());
                  x.AddProfile(new CategoryMP());
                  x.AddProfile(new LogMP());
                  x.AddProfile(new OrderMP());
                  x.AddProfile(new RequestMP());
                  x.AddExpressionMapping();
              });

            return config.CreateMapper();
        }
    }
}
