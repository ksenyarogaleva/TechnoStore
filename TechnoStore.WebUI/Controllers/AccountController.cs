using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ITechnicsRepository technics;

        public AccountController(ITechnicsRepository technicsRepository)
        {
            this.technics = technicsRepository;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.technics.Users.FirstOrDefault(u => u.Email == model.UserName && u.Password == model.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    if (user.Role.Name == "Admin")
                    {
                        return RedirectToAction("List", "Admin",new {area="Admin" });
                    }
                    else
                    {
                        return RedirectToAction("List", "Technics");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No user with such login and password found.");
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
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.technics.Users.FirstOrDefault(u => u.Email == model.UserName);
                //var user = this.technics.Users.Include(u => u.Role)
                //    .FirstOrDefault(u => u.Email == model.UserName);

                if (user == null)
                {
                    user = new User() { Email = model.UserName, Password = model.Password };
                    user.RoleId = 2;
                    user.Role = this.technics.Roles.FirstOrDefault(r => r.Id == user.RoleId);

                    this.technics.SaveUser(user);
                    //this.db.Users.Add(new User { Email = model.Name, Password = model.Password, RoleId = 2 });
                    //this.db.SaveChanges();
                    user = this.technics.Users.Where(u => u.Email == model.UserName && u.Password == model.Password).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("List", "Technics");
                    }
                }
                else
                {
                    //TempData["message"] = string.Format("Such user already exists");
                    ModelState.AddModelError("", "Such user already exists.");
                }
            }
            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("List", "Technics");
        }
    }
}