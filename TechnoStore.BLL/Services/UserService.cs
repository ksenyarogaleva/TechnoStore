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
using AutoMapper;

namespace TechnoStore.BLL.Services
{
    public class UserService : IUserService
    {
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
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

                await this.unitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);

                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                await this.unitOfWork.ClientManager.CreateClient(clientProfile);

                await this.unitOfWork.SaveAsync();
                userDto.Id = clientProfile.Id;
                return new OperationDetails(true, "Registration completed.", "");
            }
            else
            {
                return new OperationDetails(false, "Such user already exists", "Email");
            }
        }

        public async Task<ClaimsIdentity> AuthenticateAsync(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            ApplicationUser user = await this.unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);
            userDto.Id = user.Id;

            if (user != null)
            {
                claim = await this.unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

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

        public async Task<IEnumerable<OrderDTO>> GetAllOrders(string userId)
        {
            var orderEntities = await this.unitOfWork.ClientManager.GetClientOrders(userId);
            List<OrderDTO> orders = new List<OrderDTO>();
            foreach (var order in orderEntities)
            {
                orders.Add(
                    new OrderDTO
                    {
                        OrderDetails = mapper.Map<OrderDetailsDTO>(order.OrderDetails),
                        Technic = mapper.Map<TechnicDTO>(order.Technic),
                    }); ;
            }

            return orders;
        }

    }
}
