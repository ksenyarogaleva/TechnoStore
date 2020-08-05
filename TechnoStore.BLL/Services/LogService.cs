using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.BLL.Services
{
    public class LogService : ILogService
    {
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        public LogService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public int Count()
        {
            return Task.Run(async () =>
            await this.unitOfWork.Logs.CountAsync()).Result;
        }

        public void DeleteAllLogs()
        {
            Task.Run(async () =>
            await this.unitOfWork.Logs.DeleteAllAsync());
        }

        public void DeleteLog(int id)
        {
            Task.Run(async () =>
            await this.unitOfWork.Logs.DeleteAsync(id));
        }

        public IEnumerable<LogDTO> GetAllLogs()
        {
            var logs = Task.Run(async () =>
                  await this.unitOfWork.Logs.GetAllAsync()).Result;

            return mapper.Map<IEnumerable<LogDTO>>(logs);
        }

        public LogDTO GetLog(int id)
        {
            var log = Task.Run(async () =>
              await this.unitOfWork.Logs.GetSingleAsync(id)).Result;

            return mapper.Map<LogDTO>(log);
        }

    }
}
