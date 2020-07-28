using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Entities;
using TechnoStore.Common.Infrastructure;
using TechnoStore.DAL.Interfaces;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace TechnoStore.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationDetails> CreateAsync(UserDTO userDto)
        {
            ApplicationUser user = await this.unitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user is null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await this.unitOfWork.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                // add role
                await this.unitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                

                // create client profile
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                this.unitOfWork.ClientManager.CreateClient(clientProfile);

                await this.unitOfWork.SaveAsync();
                userDto.Id = clientProfile.Id;
                return new OperationDetails(true, "First step of registration completed.", "");
            }
            else
            {
                return new OperationDetails(false, "Such user already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            //find user
            ApplicationUser user = await this.unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);

            //make this user authorized and return ClaimsIdentity object
            if (user != null)
            {
                claim = await this.unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        //start db initialization
        public async Task SetInitialDataAsync(UserDTO adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await this.unitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role is null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await this.unitOfWork.RoleManager.CreateAsync(role);
                }
            }

            await CreateAsync(adminDto);
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            return await this.unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task SendEmailAsync(string userId, string subject, string body)
        {
            this.unitOfWork.UserManager.EmailService = new EmailService();
            await this.unitOfWork.UserManager.SendEmailAsync(userId, subject, body);
        }

        public async Task<OperationDetails> ConfirmEmailAsync(string userId, string code)
        {
            var result = await this.unitOfWork.UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return new OperationDetails(true, "Email confirmed", "");
            }
            else
            {
                return new OperationDetails(false, "Email wasn't confirmed.", "Email");
            }
        }

        public async Task<bool> IsEmailConfirmedAsync(string userId)
            => await this.unitOfWork.UserManager.IsEmailConfirmedAsync(userId);
    }
}
