﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
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

namespace TechnoStore.Web.Controllers
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
                    
                    

                    if (User.IsInRole("Admin"))
                    {
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);

                        return RedirectToAction("List", "Admin", new { area = "Admin" });
                    }
                    else
                    {
                        if(!await UserService.IsEmailConfirmedAsync(userDto.Id))
                        {
                            ViewBag.errorMessage = "Email haven't been confirmed.";
                            return View("Error");
                        }
                        else
                        {
                            AuthenticationManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = true
                            }, claim);

                            return RedirectToAction("List", "Technics");
                        }
                        
                    }
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
                    var code = this.UserService.GenerateEmailConfirmationTokenAsync(userDTO.Id);

                    var callbackUrl = Url.Action(
                        "ConfirmEmail", "Account", new
                        {
                            userId = userDTO.Id,
                            code = code
                        },
                        protocol: Request.Url.Scheme);

                    await this.UserService.SendEmailAsync(userDTO.Id,
                        "Confirm your account",
                        "Please, confirm your account by clicking this link: <a href=\""
                        + callbackUrl + "\">link</a>");

                    return View("DisplayEmail");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }

            }

            return View(model);
        }

        public async Task<ActionResult> ConfirmEmail(string userId,string code)
        {
            if(userId==null || code == null)
            {
                return View("Error");
            }
            var operationDetails = await UserService.ConfirmEmailAsync(userId, code);
            if (operationDetails.Succedeed)
            {
                return View("ConfirmEmail");
            }
            else
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                ViewBag.errorMessage = "Email haven't been confirmed.";
                return View("Error");
            }
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