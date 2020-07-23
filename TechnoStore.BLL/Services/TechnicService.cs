using AutoMapper;
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
    public class TechnicService : ITechnicService, IService<Technic, TechnicDTO>
    {
        protected IUnitOfWork unitOfWork;
        public TechnicService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void CreateTechnic(Technic technic)
        {
            Task.Run(async () => await this.unitOfWork.Technics.CreateAsync(technic));
        }

        public void DeleteTechnic(Technic technic)
        {
            Task.Run(async () => await this.unitOfWork.Technics.DeleteAsync(technic));
        }

        public bool Exists(Technic entity)
        {
            return Task.Run(async () =>
            await this.unitOfWork.Technics.ExistsAsync(technic => technic.Id == entity.Id)).Result;
        }

        public IEnumerable<TechnicDTO> Find(Expression<Func<Technic, bool>> predicate)
        {
            var mapper = this.GetMapperForTechnicDTO();

            var technicsList = Task.Run(async () =>
              await this.unitOfWork.Technics.FindAsync(predicate)).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<TechnicDTO>>(technicsList);
        }

        public IEnumerable<TechnicDTO> GetAll()
        {
            var mapper = this.GetMapperForTechnicDTO();

            var technicsList = Task.Run(async () =>
               await this.unitOfWork.Technics.GetAllAsync()).Result.OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<TechnicDTO>>(technicsList);
        }

        public TechnicViewModel GetTechnicsList(int pageSize, int pageNumber)
        {
            var technics= Task.Run(async () =>
                await this.unitOfWork.Technics.GetAllAsync()).Result.OrderBy(t => t.Name).ToList();
            var technicViewModel = technics.GetPagedData(pageSize, pageNumber);

            return technicViewModel;
        }

        public TechnicDTO GetSingle(int id)
        {
            var mapper = this.GetMapperForTechnicDTO();

            var technic = Task.Run(async () =>
            await this.unitOfWork.Technics.GetSingleAsync(id)).Result;

            return mapper.Map<TechnicDTO>(technic);
        }

        public void UpdateTechnic(Technic technic)
        {
            Task.Run(async () => await this.unitOfWork.Technics.UpdateAsync(technic));
        }

        private IMapper GetMapperForTechnicDTO()
        {
            return new MapperConfiguration(c => c.CreateMap<Technic, TechnicDTO>()
              .ForMember("Category", opt => opt.MapFrom(i => i.Category.Name))).CreateMapper();
        }
    }
}
