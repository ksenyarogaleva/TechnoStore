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
                return RedirectToAction("List");
            }
            else
            {
                return View("UserErrorPage");
            }
        }

        [Authorize(Roles ="Admin")]
        public ActionResult List()
        {
            var error = this.repository.Logs.ToList();
            //admin's view
            return View("AdminErrorPage", this.repository.Logs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int errorId)
        {
            var deletedError = this.repository.DeleteError(errorId);
            if (deletedError != null)
            {
                TempData["errorDeleted"] = string.Format("Error \"{0}\"  that appeared {1} was deleted.", deletedError.Id,deletedError.Date);
            }

            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAll()
        {
            int errorAmount = this.repository.Logs.Count();
            if (errorAmount>0)
            {
                this.repository.DeketeAllErrors();
                TempData["errorDeleted"] = string.Format("{0} errors were deleted.", errorAmount);
            }

            return RedirectToAction("List");
        }
    }
}