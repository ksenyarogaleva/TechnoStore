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
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task SendEmailAsync(string userId, string subject, string body);
        Task<OperationDetails> ConfirmEmailAsync(string userId, string code);
        Task<bool> IsEmailConfirmedAsync(string userId);
    }
}
