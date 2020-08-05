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
using TechnoStore.Common.Infrastructure;
using TechnoStore.Common.ViewModels;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.BLL.Services
{
    public class TechnicService : ITechnicService
    {
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        public TechnicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void CreateTechnic(TechnicDTO technic)
        {
            var entity = this.ConvertDTOIntoEntity(technic);

            Task.Run(async () => await this.unitOfWork.Technics.CreateAsync(entity));
        }

        public void DeleteTechnic(TechnicDTO technic)
        {
            var entity = this.ConvertDTOIntoEntity(technic);

            Task.Run(async () => await this.unitOfWork.Technics.DeleteAsync(entity));
        }

        public bool Exists(TechnicDTO entity)
        {
            return Task.Run(async () =>
            await this.unitOfWork.Technics.ExistsAsync(technic => technic.Id == entity.Id)).Result;
        }

        public IEnumerable<TechnicDTO> Find(Expression<Func<TechnicDTO, bool>> predicate)
        {
            var expression = mapper.MapExpression<Expression<Func<Technic, bool>>>(predicate);

            var technicsList = Task.Run(async () =>
              await this.unitOfWork.Technics.FindAsync(expression)).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<TechnicDTO>>(technicsList);
        }

        public IEnumerable<TechnicDTO> GetAll()
        {
            var technicsList = Task.Run(async () =>
               await this.unitOfWork.Technics.GetAllAsync()).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<TechnicDTO>>(technicsList);
        }

        public TechnicViewModel GetTechnicsList(int pageSize, int pageNumber, List<TechnicDTO> technics)
        {
            var technicViewModel = technics.GetPagedData(pageNumber, pageSize);

            return technicViewModel;
        }

        public TechnicDTO GetSingle(int id)
        {
            var technic = Task.Run(async () =>
            await this.unitOfWork.Technics.GetSingleAsync(id)).Result;

            return mapper.Map<TechnicDTO>(technic);
        }

        public void UpdateTechnic(TechnicDTO technic)
        {
            var entity = this.ConvertDTOIntoEntity(technic);
            Task.Run(async () => await this.unitOfWork.Technics.UpdateAsync(entity));
        }


        public TechnicEditDTO GetTechnicForEdit(int id)
        {
            var technicDTO = this.GetSingle(id);
            var entity = this.ConvertDTOIntoEntity(technicDTO);
            var technicEditDTO = mapper.Map<TechnicEditDTO>(entity);

            return technicEditDTO;
        }

        public void UpdateTechnic(TechnicEditDTO technic)
        {
            var entity = mapper.Map<Technic>(technic);
            var doesExists = Task.Run(async () =>
              await this.unitOfWork.Technics.ExistsAsync(t => t.Id == entity.Id)).Result;

            if (doesExists)
            {
                Task.Run(async () => await this.unitOfWork.Technics.UpdateAsync(entity));
            }
            else
            {
                Task.Run(async () => await this.unitOfWork.Technics.CreateAsync(entity));
            }
        }

        private Technic ConvertDTOIntoEntity(TechnicDTO technic)
        {
            return new Technic
            {
                Id = technic.Id,
                Name = technic.Name,
                Description = technic.Description,
                Price = technic.Price,
                CategoryId = Task.Run(async () => await this.unitOfWork.Categories.FindAsync(cat => cat.Name.Equals(technic.Category))).Result.First().Id,
            };
        }

    }
}
