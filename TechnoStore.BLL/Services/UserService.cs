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

        public async Task<OperationDetails> Create(UserDTO userDto)
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

                return new OperationDetails(true, "Registration completed.", "");
            }
            else
            {
                return new OperationDetails(false, "Such user already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
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
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach(var roleName in roles)
            {
                var role = await this.unitOfWork.RoleManager.FindByNameAsync(roleName);
                if(role is null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await this.unitOfWork.RoleManager.CreateAsync(role);
                }
            }

            await Create(adminDto);
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }

    }
}
