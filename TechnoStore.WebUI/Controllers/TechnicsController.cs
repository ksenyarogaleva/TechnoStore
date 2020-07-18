using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnoStore.WebUI.Infrastructure.Abstract;
using TechnoStore.WebUI.Models;

namespace TechnoStore.WebUI.Controllers
{
    public class TechnicsController : Controller
    {
        private ITechnicsRepository repository;

        public TechnicsController(ITechnicsRepository technicsRepository)
        {
            this.repository = technicsRepository;
        }


        public ActionResult List(string searchString=null, string category = null)
        {

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("List", "Admin", new { area = "Admin" });
            }

            var listModel = new TechnicsListViewModel
            {
                CurrentCategory = category,
            };

            var technics = this.repository.Technics.OrderBy(t => t.Name);

            if (!string.IsNullOrWhiteSpace(category))
            {
                technics = technics.Where(t=>t.Category.Name==category).OrderBy(t => t.Name);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                technics = technics
                    .Where(t => t.Name.ToUpper()
                    .Contains(searchString.ToUpper())).OrderBy(t=>t.Name);
            }

            listModel.Technics = technics;

            return View(listModel);
        }
    }
}