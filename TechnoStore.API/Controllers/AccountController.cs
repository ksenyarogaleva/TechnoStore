using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Tokens;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.ViewModels;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using TechnoStore.Common.DTO;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using TechnoStore.Common.Infrastructure;

namespace TechnoStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private IUserService UserService { get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); } }
        private readonly IConfiguration configuration;

        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
        {
            await this.SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "User",
                };

                OperationDetails operationDetails = await UserService.CreateAsync(userDTO);

                if (operationDetails.Succedeed)
                {
                    return Ok(new { Userame = userDTO.Email });
                }
                else
                {
                    return Conflict(new { error=operationDetails.Message});
                }
            }

            string messages = string.Join(". ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            return BadRequest(messages);


        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginViewModel model)
        {
            await this.SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                var claims = await UserService.AuthenticateAsync(userDto);
                if (claims != null)
                {
                    var issuer = this.configuration["Jwt:Issuer"];
                    var audience = this.configuration["Jwt:Audience"];
                    var expiryTime = this.configuration["Jwt:SigningKey"];
                    var signKey = this.configuration["Jwt:SigningKey"];

                    var token = this.UserService.GenerateToken(claims, issuer, audience, expiryTime, signKey);

                    return Ok(new
                    {
                        user = userDto.UserName,
                        token = token,
                        expiration = token,
                        role = userDto.Role,
                    });
                }
                    
            }
            return BadRequest("Incorrect username or password.");

        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialDataAsync(new UserDTO
            {
                Email = "ksenyarogaleva87@gmail.com",
                UserName = "ksenyarogaleva87@gmail.com",
                Password = "12345670",
                Name = "Kseniya Rogaleva",
                Address = "Minsk, st.Suharevskaya 61-53",
                Role = "Admin",
            }, new List<string> { "User", "Admin" });
        }
    }
}
