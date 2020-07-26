using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.BLL.Services
{
    public class RequestService : IRequestService
    {
        protected IUnitOfWork unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void CreateRequestStatistic(RequestDTO request)
        {
            var mapper = this.MapDTOIntoRequest();
            var entity = mapper.Map<Request>(request);

            Task.Run(async () => await this.unitOfWork.Requests.CreateAsync(entity));

        }

        public bool Exists(RequestDTO request)
        {
            return Task.Run(async () =>
            await this.unitOfWork.Requests.ExistsAsync(e => e.Id == request.Id)).Result;
        }

        public IEnumerable<RequestDTO> Find(Expression<Func<RequestDTO, bool>> predicate)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
            }));

            var expression = mapper.MapExpression<Expression<Func<Request, bool>>>(predicate);

            var requests = Task.Run(async () =>
              await this.unitOfWork.Requests.FindAsync(expression)).Result;

            return mapper.Map<IEnumerable<RequestDTO>>(requests);
        }


        public IEnumerable<RequestDTO> GetAll()
        {
            var mapper = this.MapRequestIntoDTO();

            var requests = Task.Run(async () =>
               await this.unitOfWork.Requests.GetAllAsync()).Result;

            return mapper.Map<IEnumerable<RequestDTO>>(requests);
        }

        public RequestDTO GetSingle(int id)
        {
            var mapper = this.MapRequestIntoDTO();

            var request = Task.Run(async () =>
            await this.unitOfWork.Requests.GetSingleAsync(id)).Result;

            return mapper.Map<RequestDTO>(request);
        }

        public void UpdateRequestStatistic(RequestDTO request)
        {
            var entity = this.MapDTOIntoRequest().Map<Request>(request);

            Task.Run(async () => await this.unitOfWork.Requests.UpdateAsync(entity));
        }

        private IMapper MapRequestIntoDTO()
            => new MapperConfiguration(c => c.CreateMap<Request, RequestDTO>()).CreateMapper();
        

        private IMapper MapDTOIntoRequest()
        => new MapperConfiguration(c => c.CreateMap<RequestDTO, Request>()).CreateMapper();
    }
}
