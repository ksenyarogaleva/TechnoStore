using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.BLL.Interfaces
{
    public interface IUserService:IDisposable
    {
        Task<OperationDetails> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto);
        Task SetInitialDataAsync(UserDTO adminDto, List<string> roles);
    }
}
