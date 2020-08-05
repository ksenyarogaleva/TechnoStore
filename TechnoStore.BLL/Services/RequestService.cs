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
        protected IMapper mapper;

        public RequestService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void CreateRequestStatistic(RequestDTO request)
        {
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
            var expression = mapper.MapExpression<Expression<Func<Request, bool>>>(predicate);

            var requests = Task.Run(async () =>
              await this.unitOfWork.Requests.FindAsync(expression)).Result;

            return mapper.Map<IEnumerable<RequestDTO>>(requests);
        }


        public IEnumerable<RequestDTO> GetAll()
        {
            var requests = Task.Run(async () =>
               await this.unitOfWork.Requests.GetAllAsync()).Result;

            return mapper.Map<IEnumerable<RequestDTO>>(requests);
        }

        public RequestDTO GetSingle(int id)
        {
            var request = Task.Run(async () =>
            await this.unitOfWork.Requests.GetSingleAsync(id)).Result;

            return mapper.Map<RequestDTO>(request);
        }

        public void UpdateRequestStatistic(RequestDTO request)
        {
            var entity = mapper.Map<Request>(request);

            Task.Run(async () => await this.unitOfWork.Requests.UpdateAsync(entity));
        }   

    }
}
