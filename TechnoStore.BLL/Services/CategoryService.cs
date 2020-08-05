using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
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
    public class CategoryService : ICategoryService
    {
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool Exists(CategoryDTO entity)
        {
            return Task.Run(async () =>
            await this.unitOfWork.Categories.ExistsAsync(category => category.Id == entity.Id)).Result;
        }

        public IEnumerable<CategoryDTO> Find(Expression<Func<CategoryDTO, bool>> predicate)
        {
            var expression = mapper.MapExpression<Expression<Func<Category, bool>>>(predicate);

            var categories = Task.Run(async () =>
              await this.unitOfWork.Categories.FindAsync(expression)).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = Task.Run(async () =>
              await this.unitOfWork.Categories.GetAllAsync()).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public CategoryDTO GetSingle(int id)
        {
            var category = Task.Run(async () =>
              await this.unitOfWork.Categories.GetSingleAsync(id)).Result;

            return mapper.Map<CategoryDTO>(category);
        }

    }
}
