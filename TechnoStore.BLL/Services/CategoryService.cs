using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.BLL.Services
{
    public class CategoryService : IService<Category, CategoryDTO>
    {
        private IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool Exists(Category entity)
        {
            return Task.Run(async () =>
            await this.unitOfWork.Categories.ExistsAsync(category => category.Id == entity.Id)).Result;
        }

        public IEnumerable<CategoryDTO> Find(Expression<Func<Category, bool>> predicate)
        {
            var mapper = this.GetMapperForCategoryDTO();

            var categories = Task.Run(async () =>
              await this.unitOfWork.Categories.FindAsync(predicate)).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var mapper = this.GetMapperForCategoryDTO();

            var categories = Task.Run(async () =>
              await this.unitOfWork.Categories.GetAllAsync()).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public CategoryDTO GetSingle(int id)
        {
            var mapper = this.GetMapperForCategoryDTO();

            var category = Task.Run(async () =>
              await this.unitOfWork.Categories.GetSingleAsync(id)).Result;

            return mapper.Map<CategoryDTO>(category);
        }

        private IMapper GetMapperForCategoryDTO()
        {
            return new MapperConfiguration(c => c.CreateMap<Category, CategoryDTO>()).CreateMapper();
        }
    }
}
