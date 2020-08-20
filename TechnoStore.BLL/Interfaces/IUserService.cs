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
        Task<IEnumerable<Claim>> AuthenticateAsync(UserDTO userDto);
        string GenerateToken(IEnumerable<Claim> claims, string issuer, string audience, string expiryTime, string signKey);
        Task SetInitialDataAsync(UserDTO adminDto, List<string> roles);
        Task<IEnumerable<OrderDTO>> GetAllOrders(string userId);
       
    }
}
