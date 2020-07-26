using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;
using TechnoStore.DAL.Interfaces;

namespace TechnoStore.BLL.Services
{
    public class LogService : ILogService
    {
        protected IUnitOfWork unitOfWork;
        public LogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

            return this.MapLogIntoDTO().Map<IEnumerable<LogDTO>>(logs);
        }

        public LogDTO GetLog(int id)
        {
            var log = Task.Run(async () =>
              await this.unitOfWork.Logs.GetSingleAsync(id)).Result;

            return this.MapLogIntoDTO().Map<LogDTO>(log);
        }

        private IMapper MapLogIntoDTO()
            => new MapperConfiguration(c => c.CreateMap<Log, LogDTO>()).CreateMapper();
    }
}
