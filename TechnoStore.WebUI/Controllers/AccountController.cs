using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechnoStore.BLL.Interfaces;
using TechnoStore.Common.DTO;
using TechnoStore.Common.Infrastructure;
using TechnoStore.Common.ViewModels;

namespace TechnoStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService { get { return HttpContext.GetOwinContext().GetUserManager<IUserService>(); } }

        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            await this.SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.AuthenticateAsync(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("List", "Technics");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("List", "Technics");
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
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

                //add new user to the db
                OperationDetails operationDetails = await UserService.CreateAsync(userDTO);

                if (operationDetails.Succedeed)
                {
                    return View("SuccessRegistration");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }

            }

            return View(model);
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