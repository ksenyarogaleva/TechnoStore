using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models.Entities;

namespace TechnoStore.WebUI.Areas.Error.Controllers
{
    public class ErrorController : Controller
    {
        private ITechnicsRepository repository;

        public ErrorController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }
        
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                //тут для админа короче перенаправление
                return RedirectToAction("List");
            }
            else
            {
                //users view
                return View("ErrorUserPage", this.repository.Exceptions.Last());
            }
        }

        [Authorize(Roles ="Admin")]
        public ActionResult List()
        {
            //admin's view
            return View("Error", this.repository.Exceptions);
        }
    }
}