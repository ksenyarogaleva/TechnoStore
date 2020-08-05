using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TechnoStore.BLL.AutoMapper;
using TechnoStore.BLL.Interfaces;
using TechnoStore.BLL.Services;
using TechnoStore.Common.DTO;
using TechnoStore.DAL.Repositories;

namespace TechnoStore.Tests
{
    [TestFixture]
    public class TechnicServiceTests
    {
        [Test]
        public void GetTechnicsList()
        {
            var uow = new UnitOfWork();
            var mapper = AutoMapperConfiguration.GetMapperConfiguration();
            ITechnicService technicService = new TechnicService(uow,mapper);
            var technicList = new List<TechnicDTO>
            {
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
            };
            int pageSize = 3;
            int pageNumber = 2;

            var result = technicService.GetTechnicsList(pageSize, pageNumber, technicList);

            Assert.AreEqual(1, result.Technics.Count());
        }
    }
}
