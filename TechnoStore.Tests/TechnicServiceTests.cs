using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
            ITechnicService technicService = new TechnicService(uow);
            var technicList = new List<TechnicDTO>
            {
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
                new TechnicDTO { Id = 1, Category = "Phones", Description = "Cool phone", Name = "IPhone 7 Rose gold", Price = 519.99M },
            };

            var result = technicService.GetTechnicsList(pageSize, pageNumber, technicList);

            Assert.AreEqual(1, result.Technics.Count());
        }
    }
}
