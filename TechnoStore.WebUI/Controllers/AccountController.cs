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
        //private ITechnicsRepository technics;

        //public AccountController(ITechnicsRepository technicsRepository)
        //{
        //    this.technics = technicsRepository;
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = this.technics.Users.FirstOrDefault(u => u.Email == model.UserName && u.Password == model.Password);

        //        if (user != null)
        //        {
        //            FormsAuthentication.SetAuthCookie(model.UserName, true);
        //            if (user.Role.Name == "Admin")
        //            {
        //                return RedirectToAction("List", "Admin",new {area="Admin" });
        //            }
        //            else
        //            {
        //                return RedirectToAction("List", "Technics");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "No user with such login and password found.");
        //        }
        //    }

        //    return View(model);
        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = this.technics.Users.FirstOrDefault(u => u.Email == model.UserName);
        //        //var user = this.technics.Users.Include(u => u.Role)
        //        //    .FirstOrDefault(u => u.Email == model.UserName);

        //        if (user == null)
        //        {
        //            user = new User() { Email = model.UserName, Password = model.Password };
        //            user.RoleId = 2;
        //            user.Role = this.technics.Roles.FirstOrDefault(r => r.Id == user.RoleId);

        //            this.technics.SaveUser(user);
        //            //this.db.Users.Add(new User { Email = model.Name, Password = model.Password, RoleId = 2 });
        //            //this.db.SaveChanges();
        //            user = this.technics.Users.Where(u => u.Email == model.UserName && u.Password == model.Password).FirstOrDefault();
        //            if (user != null)
        //            {
        //                FormsAuthentication.SetAuthCookie(model.UserName, true);
        //                return RedirectToAction("List", "Technics");
        //            }
        //        }
        //        else
        //        {
        //            //TempData["message"] = string.Format("Such user already exists");
        //            ModelState.AddModelError("", "Such user already exists.");
        //        }
        //    }
        //    return View(model);
        //}

        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("List", "Technics");
        }

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
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
                else
                {
                    //AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("List", "Technics");
                }
            }

            return View(model);
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
                OperationDetails operationDetails = await UserService.Create(userDTO);

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
            await UserService.SetInitialData(new UserDTO
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