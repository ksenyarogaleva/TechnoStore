using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.BLL.Interfaces;

namespace TechnoStore.Web.Areas.Error.Controllers
{
    public class ErrorController : Controller
    {
        private ILogService service;
        public ErrorController(ILogService logService)
        {
            this.service = logService;
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
            var logs = this.service.GetAllLogs();

            return View("AdminErrorPage", logs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int errorId)
        {
            var deletedLog = this.service.GetLog(errorId);
            if (deletedLog != null)
            {
                TempData["errorDeleted"] = string.Format("Error \"{0}\"  that appeared {1} was deleted.", deletedLog.Id,deletedLog.Date);
            }

            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAll()
        {
            int logsAmount = this.service.Count();
            if (logsAmount > 0)
            {
                this.service.DeleteAllLogs();
                TempData["errorDeleted"] = string.Format("{0} errors were deleted.", logsAmount);
            }

            return RedirectToAction("List");
        }
    }
}