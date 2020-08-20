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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;

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

        public async Task<IEnumerable<Claim>> AuthenticateAsync(UserDTO userDto)
        {
            IEnumerable<Claim> claims = null;

            ApplicationUser user = await this.unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);
            userDto.Id = user.Id;

            if (user != null)
            {
                //claim = await this.unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                 claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,userDto.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,userDto.Role),
                };
            
            }
            return claims;
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

        public string GenerateToken(IEnumerable<Claim> claims, string issuer, string audience, string expiryTime,string signKey)
        {
            var signInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signKey));

            int expiryTimeInMinutes = Convert.ToInt32(expiryTime);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryTimeInMinutes),
                signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
